use SEExamen2021;

CREATE TABLE te_clientes (
    cli_codigo_cliente INT PRIMARY KEY IDENTITY (1, 1),
    cli_nombre1 VARCHAR (40) NOT NULL,
    cli_nombre2 VARCHAR (40) NULL,
	cli_apellido1 VARCHAR (40) NOT NULL,
    cli_apellido2 VARCHAR (40) NULL,
	cli_apellido_casada VARCHAR (40) NULL,
	cli_direccion VARCHAR (120) NULL,
	cli_telefono1 int default 0,
	cli_telefono2 int default 0,
	cli_identificacion VARCHAR(25) NOT NULL,
    cli_fecha_nacimiento DATETIME,
    
);

insert into te_clientes (cli_nombre1 ,cli_nombre2 ,cli_apellido1,cli_apellido2 ,cli_direccion ,cli_telefono1 ,cli_telefono2 ,cli_identificacion ,cli_fecha_nacimiento) 
values ('Hector','Aaron','Juarez','Tax','Zona 6', '35098369','22704282','2963595050101',convert(datetime, '12/06/1995', 101)); 

select * from te_clientes;

create unique nonclustered index I_cli_identificacion
on te_clientes(cli_identificacion);







