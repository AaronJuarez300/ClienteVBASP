using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Globalization;
using System.Threading;

namespace Cliente.Classes
{
    public class Cliente
    {
        public static string InsertCliente(string item_)
        {
            DataTable item__ = Deserialize(item_);
            DataRow item = item__.Rows[0];
            string res;

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                Console.WriteLine(item);
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    conn.Open();
                    da.SelectCommand = new SqlCommand("[sp_mantenimiento_clientes]", conn);
                    da.SelectCommand.Parameters.AddWithValue("@i_tipo", 2);
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_codigo_cliente", 1);
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_nombre1", item.Field<string>("cli_nombre1"));
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_nombre2", item.Field<string>("cli_nombre2"));
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_apellido1", item.Field<string>("cli_apellido1"));
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_apellido2", item.Field<string>("cli_apellido2"));
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_apellido_casada", item.Field<string>("cli_apellido_casada"));
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_direccion", item.Field<string>("cli_direccion"));
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_telefono1", item.Field<string>("cli_telefono1"));
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_telefono2", item.Field<string>("cli_telefono2"));
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_identificacion", item.Field<string>("cli_identificacion"));
                    da.SelectCommand.Parameters.AddWithValue("@i_cli_fecha_nacimiento", item.Field<string>("cli_fecha_nacimiento"));

                   
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    conn.Dispose();
                    conn.Close();
                }
                res = "{" + String.Format("\"res\": {0}", "\"OK\"") + "}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                res = "{" + String.Format("\"res\": {0}", "ERROR") + "}";
            }
            return res;
        }

        public static string getClient(string item_)
        {
            string res;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    conn.Open();
                    da.SelectCommand = new SqlCommand("[sp_get_clientes]", conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                    conn.Dispose();
                    conn.Close();
                }
                res = Serialize(dt);
            }
            catch (Exception ex)
            {
                res = "{" + String.Format("\"Error\": {0}", "ERROR") + "}";
            }
            return res;
        }

        public static DataTable Deserialize(string item)
        {
            DataTable dt = new DataTable();

            string[] jsonStringArray = Regex.Split(item.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                //string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                string[] jsonStringData = r.Split(jSA.Replace("{", "").Replace("}", ""));
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                Regex r = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                string[] RowData = r.Split(jSA.Replace("{", "").Replace("}", ""));
                //string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        if (RowDataString == "null" || RowDataString == "")
                        {
                            nr[RowColumns] = null;
                        }
                        else
                        {
                            nr[RowColumns] = RowDataString;
                        }

                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }

        public static string Serialize(DataTable dt)
        {
            CultureInfo ci = new CultureInfo("es-GT");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }

            return serializer.Serialize(rows);
        }

    }
}