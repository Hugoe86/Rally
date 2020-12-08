﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace datos_trazabilidad
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class Sistema_TrazabilidadEntities : DbContext
    {
        public Sistema_TrazabilidadEntities()
            : base("name=Sistema_TrazabilidadEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Apl_Accesos> Apl_Accesos { get; set; }
        public virtual DbSet<Apl_Avisos> Apl_Avisos { get; set; }
        public virtual DbSet<Apl_Cat_Bancos> Apl_Cat_Bancos { get; set; }
        public virtual DbSet<Apl_Cat_Metodo_Pago> Apl_Cat_Metodo_Pago { get; set; }
        public virtual DbSet<Apl_Cat_Parametros> Apl_Cat_Parametros { get; set; }
        public virtual DbSet<Apl_Cat_Parametros_Eventos> Apl_Cat_Parametros_Eventos { get; set; }
        public virtual DbSet<Apl_Cat_Tipo_Movimiento> Apl_Cat_Tipo_Movimiento { get; set; }
        public virtual DbSet<Apl_Empresas> Apl_Empresas { get; set; }
        public virtual DbSet<Apl_Entidades_Empresas> Apl_Entidades_Empresas { get; set; }
        public virtual DbSet<Apl_Menus> Apl_Menus { get; set; }
        public virtual DbSet<Apl_Menus_Empresa> Apl_Menus_Empresa { get; set; }
        public virtual DbSet<Apl_Modulos> Apl_Modulos { get; set; }
        public virtual DbSet<Apl_Niveles> Apl_Niveles { get; set; }
        public virtual DbSet<Apl_Notificaciones> Apl_Notificaciones { get; set; }
        public virtual DbSet<Apl_Parametros_Facturas> Apl_Parametros_Facturas { get; set; }
        public virtual DbSet<Apl_Pos_Accesos> Apl_Pos_Accesos { get; set; }
        public virtual DbSet<Apl_Rel_Usuarios_Roles> Apl_Rel_Usuarios_Roles { get; set; }
        public virtual DbSet<Apl_Roles> Apl_Roles { get; set; }
        public virtual DbSet<Apl_Roles_Sucursales> Apl_Roles_Sucursales { get; set; }
        public virtual DbSet<Apl_Sucursales> Apl_Sucursales { get; set; }
        public virtual DbSet<Apl_Tipos_Usuarios> Apl_Tipos_Usuarios { get; set; }
        public virtual DbSet<Apl_Usuarios> Apl_Usuarios { get; set; }
        public virtual DbSet<Apl_Usuarios_Notificaciones> Apl_Usuarios_Notificaciones { get; set; }
        public virtual DbSet<Apl_Usuarios_Password> Apl_Usuarios_Password { get; set; }
        public virtual DbSet<Cat_Con_Centro_Costos> Cat_Con_Centro_Costos { get; set; }
        public virtual DbSet<Cat_Con_Clientes_Cuentas_Contables_CC> Cat_Con_Clientes_Cuentas_Contables_CC { get; set; }
        public virtual DbSet<Cat_Con_Cuentas_Contables> Cat_Con_Cuentas_Contables { get; set; }
        public virtual DbSet<Cat_Con_Cuentas_Contables_Sat> Cat_Con_Cuentas_Contables_Sat { get; set; }
        public virtual DbSet<Cat_Con_Impuestos_Cuentas_Contables_CC> Cat_Con_Impuestos_Cuentas_Contables_CC { get; set; }
        public virtual DbSet<Cat_Con_Niveles> Cat_Con_Niveles { get; set; }
        public virtual DbSet<Cat_Nom_Ciudades> Cat_Nom_Ciudades { get; set; }
        public virtual DbSet<Cat_Nom_Codigos_Postales> Cat_Nom_Codigos_Postales { get; set; }
        public virtual DbSet<Cat_Nom_Departamentos> Cat_Nom_Departamentos { get; set; }
        public virtual DbSet<Cat_Nom_Estados> Cat_Nom_Estados { get; set; }
        public virtual DbSet<Cat_Nom_Localidades> Cat_Nom_Localidades { get; set; }
        public virtual DbSet<Cat_Nom_Municipios_Localidades> Cat_Nom_Municipios_Localidades { get; set; }
        public virtual DbSet<Cat_Nom_Paises> Cat_Nom_Paises { get; set; }
        public virtual DbSet<Cat_Nom_Regimen_Fiscal> Cat_Nom_Regimen_Fiscal { get; set; }
        public virtual DbSet<Cat_Participantes> Cat_Participantes { get; set; }
        public virtual DbSet<Cat_Participantes_Adjuntos> Cat_Participantes_Adjuntos { get; set; }
        public virtual DbSet<Cat_Procesos_Sistema> Cat_Procesos_Sistema { get; set; }
        public virtual DbSet<Cat_Rel_Usuarios_Procesos_Sistema> Cat_Rel_Usuarios_Procesos_Sistema { get; set; }
        public virtual DbSet<Cat_Relacion_Participante_Vehiculo> Cat_Relacion_Participante_Vehiculo { get; set; }
        public virtual DbSet<Cat_Responsables> Cat_Responsables { get; set; }
        public virtual DbSet<Cat_Sat_Bancos> Cat_Sat_Bancos { get; set; }
        public virtual DbSet<Cat_Tipos_Notificaciones> Cat_Tipos_Notificaciones { get; set; }
        public virtual DbSet<Cat_Vehiculos> Cat_Vehiculos { get; set; }
        public virtual DbSet<Cat_Vehiculos_Documentos> Cat_Vehiculos_Documentos { get; set; }
        public virtual DbSet<Ope_Con_Cuentas_Contables_Centro_Costo> Ope_Con_Cuentas_Contables_Centro_Costo { get; set; }
        public virtual DbSet<Ope_Eventos> Ope_Eventos { get; set; }
        public virtual DbSet<Ope_Eventos_Actividades> Ope_Eventos_Actividades { get; set; }
        public virtual DbSet<Ope_Eventos_Categorias> Ope_Eventos_Categorias { get; set; }
        public virtual DbSet<Ope_Eventos_Horas_Zero> Ope_Eventos_Horas_Zero { get; set; }
        public virtual DbSet<Ope_Eventos_Jornadas> Ope_Eventos_Jornadas { get; set; }
        public virtual DbSet<Ope_Eventos_Pnt_Ctrl_Categ_Tiempos> Ope_Eventos_Pnt_Ctrl_Categ_Tiempos { get; set; }
        public virtual DbSet<Ope_Eventos_Pnt_Ctrl_Operador> Ope_Eventos_Pnt_Ctrl_Operador { get; set; }
        public virtual DbSet<Ope_Eventos_Puntos_Control> Ope_Eventos_Puntos_Control { get; set; }
        public virtual DbSet<Ope_Eventos_Redes_Sociales> Ope_Eventos_Redes_Sociales { get; set; }
        public virtual DbSet<Ope_Eventos_Registro_Tiempo> Ope_Eventos_Registro_Tiempo { get; set; }
        public virtual DbSet<Ope_Eventos_Registro_Tiempo_Backup> Ope_Eventos_Registro_Tiempo_Backup { get; set; }
        public virtual DbSet<Ope_Eventos_Sincronizacion> Ope_Eventos_Sincronizacion { get; set; }
        public virtual DbSet<Ope_Eventos_Vehiculo_Participante> Ope_Eventos_Vehiculo_Participante { get; set; }
        public virtual DbSet<Sys_Accesos> Sys_Accesos { get; set; }
        public virtual DbSet<Tra_Cat_Config_Items_Table_Show> Tra_Cat_Config_Items_Table_Show { get; set; }
        public virtual DbSet<Tra_Cat_Estatus> Tra_Cat_Estatus { get; set; }
        public virtual DbSet<Ope_Eventos_Horas_ZeroTmp> Ope_Eventos_Horas_ZeroTmp { get; set; }
        public virtual DbSet<Ope_Eventos_Registro_TiempoTmp> Ope_Eventos_Registro_TiempoTmp { get; set; }
    
        public virtual int Activar_Usuario(Nullable<int> id_Usuario)
        {
            var id_UsuarioParameter = id_Usuario.HasValue ?
                new ObjectParameter("Id_Usuario", id_Usuario) :
                new ObjectParameter("Id_Usuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Activar_Usuario", id_UsuarioParameter);
        }
    
        public virtual int Block_User(string user)
        {
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Block_User", userParameter);
        }
    
        public virtual ObjectResult<Email_Confirmation_Result> Email_Confirmation(Nullable<int> id_Usuario)
        {
            var id_UsuarioParameter = id_Usuario.HasValue ?
                new ObjectParameter("Id_Usuario", id_Usuario) :
                new ObjectParameter("Id_Usuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Email_Confirmation_Result>("Email_Confirmation", id_UsuarioParameter);
        }
    
        public virtual ObjectResult<Empleado_Nombre_Password_Result> Empleado_Nombre_Password(Nullable<int> iD_Empresa, string usuario, string correo)
        {
            var iD_EmpresaParameter = iD_Empresa.HasValue ?
                new ObjectParameter("ID_Empresa", iD_Empresa) :
                new ObjectParameter("ID_Empresa", typeof(int));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("Usuario", usuario) :
                new ObjectParameter("Usuario", typeof(string));
    
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Empleado_Nombre_Password_Result>("Empleado_Nombre_Password", iD_EmpresaParameter, usuarioParameter, correoParameter);
        }
    
        public virtual ObjectResult<Get_User_Login_Data_Result> Get_User_Login_Data(string user)
        {
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Get_User_Login_Data_Result>("Get_User_Login_Data", userParameter);
        }
    
        public virtual int Increment_No_Intentos_User(string user)
        {
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Increment_No_Intentos_User", userParameter);
        }
    
        public virtual int Insert_No_Intento(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_No_Intento", iDParameter);
        }
    
        public virtual int Insert_Users_Passwords(Nullable<int> id_Empresa, Nullable<int> id_User, string contrasenia)
        {
            var id_EmpresaParameter = id_Empresa.HasValue ?
                new ObjectParameter("Id_Empresa", id_Empresa) :
                new ObjectParameter("Id_Empresa", typeof(int));
    
            var id_UserParameter = id_User.HasValue ?
                new ObjectParameter("Id_User", id_User) :
                new ObjectParameter("Id_User", typeof(int));
    
            var contraseniaParameter = contrasenia != null ?
                new ObjectParameter("Contrasenia", contrasenia) :
                new ObjectParameter("Contrasenia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Insert_Users_Passwords", id_EmpresaParameter, id_UserParameter, contraseniaParameter);
        }
    
        public virtual ObjectResult<Nombre_Rol_Empleado_Result> Nombre_Rol_Empleado(Nullable<int> iD_Usuario)
        {
            var iD_UsuarioParameter = iD_Usuario.HasValue ?
                new ObjectParameter("ID_Usuario", iD_Usuario) :
                new ObjectParameter("ID_Usuario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nombre_Rol_Empleado_Result>("Nombre_Rol_Empleado", iD_UsuarioParameter);
        }
    
        public virtual ObjectResult<string> Parameter_No_Intentos_Empresa(Nullable<int> id_Empresa)
        {
            var id_EmpresaParameter = id_Empresa.HasValue ?
                new ObjectParameter("Id_Empresa", id_Empresa) :
                new ObjectParameter("Id_Empresa", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Parameter_No_Intentos_Empresa", id_EmpresaParameter);
        }
    
        public virtual int Restart_No_Intento(Nullable<int> iD)
        {
            var iDParameter = iD.HasValue ?
                new ObjectParameter("ID", iD) :
                new ObjectParameter("ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Restart_No_Intento", iDParameter);
        }
    
        public virtual int Restart_No_Intentos(string user)
        {
            var userParameter = user != null ?
                new ObjectParameter("User", user) :
                new ObjectParameter("User", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Restart_No_Intentos", userParameter);
        }
    
        public virtual ObjectResult<Retrieve_User_Account_Result> Retrieve_User_Account(string correo)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Retrieve_User_Account_Result>("Retrieve_User_Account", correoParameter);
        }
    
        public virtual ObjectResult<string> Roles_Empresa_Empleado(Nullable<int> iD_Empresa)
        {
            var iD_EmpresaParameter = iD_Empresa.HasValue ?
                new ObjectParameter("ID_Empresa", iD_Empresa) :
                new ObjectParameter("ID_Empresa", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Roles_Empresa_Empleado", iD_EmpresaParameter);
        }
    
        public virtual ObjectResult<Select_Menus_Result> Select_Menus()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Menus_Result>("Select_Menus");
        }
    
        public virtual ObjectResult<Select_Submenus_Result> Select_Submenus(Nullable<int> rol, string parent)
        {
            var rolParameter = rol.HasValue ?
                new ObjectParameter("Rol", rol) :
                new ObjectParameter("Rol", typeof(int));
    
            var parentParameter = parent != null ?
                new ObjectParameter("Parent", parent) :
                new ObjectParameter("Parent", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Select_Submenus_Result>("Select_Submenus", rolParameter, parentParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int Update_User_Password(Nullable<int> id_User, string contrasenia)
        {
            var id_UserParameter = id_User.HasValue ?
                new ObjectParameter("Id_User", id_User) :
                new ObjectParameter("Id_User", typeof(int));
    
            var contraseniaParameter = contrasenia != null ?
                new ObjectParameter("Contrasenia", contrasenia) :
                new ObjectParameter("Contrasenia", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Update_User_Password", id_UserParameter, contraseniaParameter);
        }
    
        public virtual ObjectResult<User_Retrieve_Data_Token_Result> User_Retrieve_Data_Token(Nullable<int> id_User)
        {
            var id_UserParameter = id_User.HasValue ?
                new ObjectParameter("Id_User", id_User) :
                new ObjectParameter("Id_User", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<User_Retrieve_Data_Token_Result>("User_Retrieve_Data_Token", id_UserParameter);
        }
    
        public virtual int User_Set_Token_Datetime(Nullable<int> id_User, string token, Nullable<System.DateTime> expira_Token)
        {
            var id_UserParameter = id_User.HasValue ?
                new ObjectParameter("Id_User", id_User) :
                new ObjectParameter("Id_User", typeof(int));
    
            var tokenParameter = token != null ?
                new ObjectParameter("Token", token) :
                new ObjectParameter("Token", typeof(string));
    
            var expira_TokenParameter = expira_Token.HasValue ?
                new ObjectParameter("Expira_Token", expira_Token) :
                new ObjectParameter("Expira_Token", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("User_Set_Token_Datetime", id_UserParameter, tokenParameter, expira_TokenParameter);
        }
    
        public virtual ObjectResult<string> User_Three_Lastest_Passwords(Nullable<int> id_Empresa, Nullable<int> id_User)
        {
            var id_EmpresaParameter = id_Empresa.HasValue ?
                new ObjectParameter("Id_Empresa", id_Empresa) :
                new ObjectParameter("Id_Empresa", typeof(int));
    
            var id_UserParameter = id_User.HasValue ?
                new ObjectParameter("Id_User", id_User) :
                new ObjectParameter("Id_User", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("User_Three_Lastest_Passwords", id_EmpresaParameter, id_UserParameter);
        }
    
        public virtual ObjectResult<string> SP_REGISTRAR_CONTRARECIBO(Nullable<int> empresa_ID, Nullable<int> no_Orden_Compra, Nullable<int> proveedor_ID, Nullable<int> estatus_ID, Nullable<System.DateTime> fecha_Recepcion, Nullable<System.DateTime> fecha_Pago, string observaciones, string usuario_Creo, string detalles)
        {
            var empresa_IDParameter = empresa_ID.HasValue ?
                new ObjectParameter("Empresa_ID", empresa_ID) :
                new ObjectParameter("Empresa_ID", typeof(int));
    
            var no_Orden_CompraParameter = no_Orden_Compra.HasValue ?
                new ObjectParameter("No_Orden_Compra", no_Orden_Compra) :
                new ObjectParameter("No_Orden_Compra", typeof(int));
    
            var proveedor_IDParameter = proveedor_ID.HasValue ?
                new ObjectParameter("Proveedor_ID", proveedor_ID) :
                new ObjectParameter("Proveedor_ID", typeof(int));
    
            var estatus_IDParameter = estatus_ID.HasValue ?
                new ObjectParameter("Estatus_ID", estatus_ID) :
                new ObjectParameter("Estatus_ID", typeof(int));
    
            var fecha_RecepcionParameter = fecha_Recepcion.HasValue ?
                new ObjectParameter("Fecha_Recepcion", fecha_Recepcion) :
                new ObjectParameter("Fecha_Recepcion", typeof(System.DateTime));
    
            var fecha_PagoParameter = fecha_Pago.HasValue ?
                new ObjectParameter("Fecha_Pago", fecha_Pago) :
                new ObjectParameter("Fecha_Pago", typeof(System.DateTime));
    
            var observacionesParameter = observaciones != null ?
                new ObjectParameter("Observaciones", observaciones) :
                new ObjectParameter("Observaciones", typeof(string));
    
            var usuario_CreoParameter = usuario_Creo != null ?
                new ObjectParameter("Usuario_Creo", usuario_Creo) :
                new ObjectParameter("Usuario_Creo", typeof(string));
    
            var detallesParameter = detalles != null ?
                new ObjectParameter("Detalles", detalles) :
                new ObjectParameter("Detalles", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SP_REGISTRAR_CONTRARECIBO", empresa_IDParameter, no_Orden_CompraParameter, proveedor_IDParameter, estatus_IDParameter, fecha_RecepcionParameter, fecha_PagoParameter, observacionesParameter, usuario_CreoParameter, detallesParameter);
        }
    
        public virtual ObjectResult<Consultar_Contenedores_Result> Consultar_Contenedores(Nullable<int> empresa_ID, Nullable<int> sucursal_ID, string tipo_Producto, Nullable<int> producto_ID, Nullable<int> almacen, Nullable<int> ubicacion)
        {
            var empresa_IDParameter = empresa_ID.HasValue ?
                new ObjectParameter("Empresa_ID", empresa_ID) :
                new ObjectParameter("Empresa_ID", typeof(int));
    
            var sucursal_IDParameter = sucursal_ID.HasValue ?
                new ObjectParameter("Sucursal_ID", sucursal_ID) :
                new ObjectParameter("Sucursal_ID", typeof(int));
    
            var tipo_ProductoParameter = tipo_Producto != null ?
                new ObjectParameter("Tipo_Producto", tipo_Producto) :
                new ObjectParameter("Tipo_Producto", typeof(string));
    
            var producto_IDParameter = producto_ID.HasValue ?
                new ObjectParameter("Producto_ID", producto_ID) :
                new ObjectParameter("Producto_ID", typeof(int));
    
            var almacenParameter = almacen.HasValue ?
                new ObjectParameter("Almacen", almacen) :
                new ObjectParameter("Almacen", typeof(int));
    
            var ubicacionParameter = ubicacion.HasValue ?
                new ObjectParameter("Ubicacion", ubicacion) :
                new ObjectParameter("Ubicacion", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Consultar_Contenedores_Result>("Consultar_Contenedores", empresa_IDParameter, sucursal_IDParameter, tipo_ProductoParameter, producto_IDParameter, almacenParameter, ubicacionParameter);
        }
    
        public virtual ObjectResult<string> Consultar_Informacion_Inventario(string no_Parte, Nullable<int> empresa_ID, string fecha_Inicio, string fecha_Termino)
        {
            var no_ParteParameter = no_Parte != null ?
                new ObjectParameter("No_Parte", no_Parte) :
                new ObjectParameter("No_Parte", typeof(string));
    
            var empresa_IDParameter = empresa_ID.HasValue ?
                new ObjectParameter("Empresa_ID", empresa_ID) :
                new ObjectParameter("Empresa_ID", typeof(int));
    
            var fecha_InicioParameter = fecha_Inicio != null ?
                new ObjectParameter("Fecha_Inicio", fecha_Inicio) :
                new ObjectParameter("Fecha_Inicio", typeof(string));
    
            var fecha_TerminoParameter = fecha_Termino != null ?
                new ObjectParameter("Fecha_Termino", fecha_Termino) :
                new ObjectParameter("Fecha_Termino", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Consultar_Informacion_Inventario", no_ParteParameter, empresa_IDParameter, fecha_InicioParameter, fecha_TerminoParameter);
        }
    
        public virtual ObjectResult<string> queryInvPlasticOmnion(string no_Parte, string no_Parte_Cliente, Nullable<int> empresa_ID, string fecha_Inicio, string fecha_Fin)
        {
            var no_ParteParameter = no_Parte != null ?
                new ObjectParameter("No_Parte", no_Parte) :
                new ObjectParameter("No_Parte", typeof(string));
    
            var no_Parte_ClienteParameter = no_Parte_Cliente != null ?
                new ObjectParameter("No_Parte_Cliente", no_Parte_Cliente) :
                new ObjectParameter("No_Parte_Cliente", typeof(string));
    
            var empresa_IDParameter = empresa_ID.HasValue ?
                new ObjectParameter("Empresa_ID", empresa_ID) :
                new ObjectParameter("Empresa_ID", typeof(int));
    
            var fecha_InicioParameter = fecha_Inicio != null ?
                new ObjectParameter("Fecha_Inicio", fecha_Inicio) :
                new ObjectParameter("Fecha_Inicio", typeof(string));
    
            var fecha_FinParameter = fecha_Fin != null ?
                new ObjectParameter("Fecha_Fin", fecha_Fin) :
                new ObjectParameter("Fecha_Fin", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("queryInvPlasticOmnion", no_ParteParameter, no_Parte_ClienteParameter, empresa_IDParameter, fecha_InicioParameter, fecha_FinParameter);
        }
    
        public virtual ObjectResult<sp_Consulta_Recepcion_Material_Pendiente_Result> sp_Consulta_Recepcion_Material_Pendiente(Nullable<int> empresa_ID, Nullable<int> sucursal_ID, Nullable<int> no_Orden_Compra)
        {
            var empresa_IDParameter = empresa_ID.HasValue ?
                new ObjectParameter("Empresa_ID", empresa_ID) :
                new ObjectParameter("Empresa_ID", typeof(int));
    
            var sucursal_IDParameter = sucursal_ID.HasValue ?
                new ObjectParameter("Sucursal_ID", sucursal_ID) :
                new ObjectParameter("Sucursal_ID", typeof(int));
    
            var no_Orden_CompraParameter = no_Orden_Compra.HasValue ?
                new ObjectParameter("No_Orden_Compra", no_Orden_Compra) :
                new ObjectParameter("No_Orden_Compra", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Consulta_Recepcion_Material_Pendiente_Result>("sp_Consulta_Recepcion_Material_Pendiente", empresa_IDParameter, sucursal_IDParameter, no_Orden_CompraParameter);
        }
    
        public virtual ObjectResult<SP_ConsultarProductosUbicacion_Result> SP_ConsultarProductosUbicacion(Nullable<int> empresa_ID, Nullable<int> sucursal_ID, Nullable<int> almacen_ID, Nullable<int> ubicacion_ID)
        {
            var empresa_IDParameter = empresa_ID.HasValue ?
                new ObjectParameter("Empresa_ID", empresa_ID) :
                new ObjectParameter("Empresa_ID", typeof(int));
    
            var sucursal_IDParameter = sucursal_ID.HasValue ?
                new ObjectParameter("Sucursal_ID", sucursal_ID) :
                new ObjectParameter("Sucursal_ID", typeof(int));
    
            var almacen_IDParameter = almacen_ID.HasValue ?
                new ObjectParameter("Almacen_ID", almacen_ID) :
                new ObjectParameter("Almacen_ID", typeof(int));
    
            var ubicacion_IDParameter = ubicacion_ID.HasValue ?
                new ObjectParameter("Ubicacion_ID", ubicacion_ID) :
                new ObjectParameter("Ubicacion_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_ConsultarProductosUbicacion_Result>("SP_ConsultarProductosUbicacion", empresa_IDParameter, sucursal_IDParameter, almacen_IDParameter, ubicacion_IDParameter);
        }
    
        public virtual ObjectResult<sp_verificar_reabastecimiento_Result> sp_verificar_reabastecimiento(Nullable<int> no_Solicitud)
        {
            var no_SolicitudParameter = no_Solicitud.HasValue ?
                new ObjectParameter("No_Solicitud", no_Solicitud) :
                new ObjectParameter("No_Solicitud", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_verificar_reabastecimiento_Result>("sp_verificar_reabastecimiento", no_SolicitudParameter);
        }
    
        public virtual ObjectResult<sp_verificar_reabastecimiento1_Result> sp_verificar_reabastecimiento1(Nullable<int> no_Solicitud)
        {
            var no_SolicitudParameter = no_Solicitud.HasValue ?
                new ObjectParameter("No_Solicitud", no_Solicitud) :
                new ObjectParameter("No_Solicitud", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_verificar_reabastecimiento1_Result>("sp_verificar_reabastecimiento1", no_SolicitudParameter);
        }
    
        public virtual ObjectResult<sp_Consultar_Detalles_Solicitud_Surtido_Result3> sp_Consultar_Detalles_Solicitud_Surtido(Nullable<int> no_Solicitud)
        {
            var no_SolicitudParameter = no_Solicitud.HasValue ?
                new ObjectParameter("No_Solicitud", no_Solicitud) :
                new ObjectParameter("No_Solicitud", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Consultar_Detalles_Solicitud_Surtido_Result3>("sp_Consultar_Detalles_Solicitud_Surtido", no_SolicitudParameter);
        }
    
        public virtual ObjectResult<sp_Consultar_Contenedores_Transferencia_Inventario_Result> sp_Consultar_Contenedores_Transferencia_Inventario(Nullable<int> empresa, Nullable<int> sucursal, Nullable<int> ubicacion, Nullable<int> no_Orden_Produccion)
        {
            var empresaParameter = empresa.HasValue ?
                new ObjectParameter("Empresa", empresa) :
                new ObjectParameter("Empresa", typeof(int));
    
            var sucursalParameter = sucursal.HasValue ?
                new ObjectParameter("Sucursal", sucursal) :
                new ObjectParameter("Sucursal", typeof(int));
    
            var ubicacionParameter = ubicacion.HasValue ?
                new ObjectParameter("Ubicacion", ubicacion) :
                new ObjectParameter("Ubicacion", typeof(int));
    
            var no_Orden_ProduccionParameter = no_Orden_Produccion.HasValue ?
                new ObjectParameter("No_Orden_Produccion", no_Orden_Produccion) :
                new ObjectParameter("No_Orden_Produccion", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Consultar_Contenedores_Transferencia_Inventario_Result>("sp_Consultar_Contenedores_Transferencia_Inventario", empresaParameter, sucursalParameter, ubicacionParameter, no_Orden_ProduccionParameter);
        }
    
        public virtual int spArbolCC(string fecha, Nullable<int> empresa_ID, Nullable<int> sucursal_ID)
        {
            var fechaParameter = fecha != null ?
                new ObjectParameter("Fecha", fecha) :
                new ObjectParameter("Fecha", typeof(string));
    
            var empresa_IDParameter = empresa_ID.HasValue ?
                new ObjectParameter("Empresa_ID", empresa_ID) :
                new ObjectParameter("Empresa_ID", typeof(int));
    
            var sucursal_IDParameter = sucursal_ID.HasValue ?
                new ObjectParameter("Sucursal_ID", sucursal_ID) :
                new ObjectParameter("Sucursal_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spArbolCC", fechaParameter, empresa_IDParameter, sucursal_IDParameter);
        }
    }
}
