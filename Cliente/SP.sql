DROP PROCEDURE IF EXISTS sp_mantenimiento_clientes;  
GO  
create procedure sp_mantenimiento_clientes 

	@i_tipo  int,
	@i_cli_codigo_cliente int null,
	@i_cli_nombre1 VARCHAR (40) null,
    @i_cli_nombre2 VARCHAR (40) = null,
	@i_cli_apellido1 VARCHAR (40) null,
    @i_cli_apellido2 VARCHAR (40) = null ,
	@i_cli_apellido_casada VARCHAR (40) = null,
	@i_cli_direccion VARCHAR (120) = null,
	@i_cli_telefono1 int =0 ,
	@i_cli_telefono2 int=0 ,
	@i_cli_identificacion VARCHAR(25) null,
    @i_cli_fecha_nacimiento varchar(100) null

 as

 Begin
 /*Consulta todos los clientes */
if @i_tipo = 1
	select cli_codigo_cliente ,cli_nombre1 ,cli_nombre2 ,cli_apellido1,cli_apellido2 ,cli_apellido_casada ,cli_direccion ,cli_telefono1 ,cli_telefono2 ,cli_identificacion ,cli_fecha_nacimiento
	from te_clientes

/*Insertar Cliente*/
Else if @i_tipo = 2
	insert into te_clientes (cli_nombre1 ,cli_nombre2 ,cli_apellido1,cli_apellido2 ,cli_apellido_casada,cli_direccion ,cli_telefono1 ,cli_telefono2 ,cli_identificacion ,cli_fecha_nacimiento) 
	values (@i_cli_nombre1,@i_cli_nombre2,@i_cli_apellido1,@i_cli_apellido2,@i_cli_apellido_casada,@i_cli_direccion, @i_cli_telefono1,@i_cli_telefono2,@i_cli_identificacion,convert(datetime,  @i_cli_fecha_nacimiento, 101)); 
/*Eliminar Cliente*/
Else if @i_tipo = 3
	delete from te_clientes
	where cli_codigo_cliente=@i_cli_codigo_cliente;
/*Consulta en base al indice*/
Else if @i_tipo = 4
	select cli_codigo_cliente ,cli_nombre1 ,cli_nombre2 ,cli_apellido1,cli_apellido2 ,cli_apellido_casada ,cli_direccion ,cli_telefono1 ,cli_telefono2 ,cli_identificacion ,cli_fecha_nacimiento
	from te_clientes where cli_identificacion = @i_cli_identificacion
/*Consulta por nombre y apellido*/
Else if @i_tipo = 5
	select cli_codigo_cliente ,cli_nombre1 ,cli_nombre2 ,cli_apellido1,cli_apellido2 ,cli_apellido_casada ,cli_direccion ,cli_telefono1 ,cli_telefono2 ,cli_identificacion ,cli_fecha_nacimiento
	from te_clientes where cli_nombre1 = @i_cli_nombre1 and cli_apellido1 = @i_cli_apellido1 
Else if @i_tipo = 6
	update te_clientes set cli_telefono1 = @i_cli_telefono1 where cli_codigo_cliente = @i_cli_codigo_cliente
	end
go