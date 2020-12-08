using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_trazabilidad.Models.Negocio
{
    public class Cls_Tra_Cat_Empleados_Negocio
    {
        public int No_Plaza { get; set; }
        public int? No_Contrato { get; set; }        
        public int Empleado_ID { get; set; }
        public int Estatus_ID { get; set; }
        public int? Usuario_ID { get; set; }
        public string Nombre { get; set; }
        public string Usuario_Creo { get; set; }
        public string Fecha_Creo { get; set; }
        public string Usuario_Modifico { get; set; }
        public string Fecha_Modifico { get; set; }
        public int? Clave { get; set; }
        public bool Es_Gerente { get; set; }
        public string Es_Gerente_Mostrar { get; set; }
        //nomina
        public string No_Empleado { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string Nombre_Completo { get; set; }
        public string Fecha_Nacimiento { get; set; }
        public string CURP { get; set; }
        public string RFC { get; set; }
        public string NSS { get; set; }
        public string Sexo { get; set; }
        public string Estado_Civil { get; set; }
        public string Lugar_Nacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public Nullable<int> Codigo_Postal { get; set; }
        public string No_Interior { get; set; }
        public string No_Exterior { get; set; }
        public string Direccion_Completa { get; set; }
        public string Localidad { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Clave_Elector { get; set; }
        public string Fecha_Ingreso { get; set; }
        public Nullable<bool> Trabaja_Domingos { get; set; }
        public string Telefono_Casa { get; set; }
        public string Telefono_Oficina { get; set; }
        public string Extencion_Telefono_Oficina { get; set; }
        public string Telefono_Emergencia { get; set; }
        public string Telefono_Emergencia_2 { get; set; }
        public string Fax { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Tipo_Empleado { get; set; }
        public string Tipo_Contrato { get; set; }
        public string No_Licencia_Manejo { get; set; }
        public string Imagen { get; set; }
        public Nullable<double> Salario_Diario { get; set; }
        public Nullable<double> Salario_Diario_Integrado { get; set; }
        public Nullable<double> Salario_Mesual_Actual { get; set; }
        public Nullable<bool> Es_Sindicalizado { get; set; }
        public string Tipo_Licencia { get; set; }
        public string Fecha_Vencimiento_Licencia { get; set; }
        public string Cartilla_Militar { get; set; }
        public string Tipo_Sangre { get; set; }
        public Nullable<bool> Empleado_Checa { get; set; }
        public Nullable<bool> Empleado_Checa_Salida { get; set; }
        public Nullable<bool> Empleado_Checa_Entrada { get; set; }
        public string Fecha_Inicia_Reloj_Checador { get; set; }
        public string Nombre_Padre { get; set; }
        public string Nombre_Madre { get; set; }
        public string Nombre_conyuge { get; set; }
        public Nullable<int> No_Hijos { get; set; }
        public string Registo_Patronal { get; set; }
        public string No_Tarjeta { get; set; }
        public string No_Cuenta_Bancaria { get; set; }
        public string Fecha_Salario_Aumento { get; set; }
        public Nullable<bool> Infonavit { get; set; }
        public string No_Infonavit { get; set; }
        public Nullable<double> Cantidad_Infonavit { get; set; }
        public string Tipo_Descuento_Infonavit { get; set; }
        public string Valor_Descuento_Infonavit { get; set; }
        public string Fecha_Inicio_Desc_Info { get; set; }
        public string Fecha_Termino_Desc_Info { get; set; }
        public Nullable<double> Total_Ahorro { get; set; }
        public string Tipo_Trabajador { get; set; }
        public string Semana_Jornada_Reducida { get; set; }
        public string Unidad_Medica_Familiar { get; set; }
        public Nullable<int> Horas_Labora { get; set; }
        public string Tipo_Salario { get; set; }
        public string Comentarios { get; set; }
        public string Comentarios_Baja { get; set; }
        public string Tipo_Baja { get; set; }
        public string Fecha_Baja { get; set; }
        public string Estatus { get; set; }
        public Nullable<double> Gastos_Medicos_Mayores { get; set; }
        public Nullable<bool> Ocupa_Transporte { get; set; }
        public Nullable<int> Puesto_ID { get; set; }
        public Nullable<int> Nivel_Estudio_ID { get; set; }
        public Nullable<int> Pais_ID { get; set; }
        public Nullable<int> Estado_ID { get; set; }
        public Nullable<int> Origen_Recurso_ID { get; set; }
        public Nullable<int> Tipo_Contrato_ID { get; set; }
        public Nullable<int> Tipo_Jornada_ID { get; set; }
        public Nullable<int> Banco_ID { get; set; }
        public Nullable<int> Tipo_Nomina_ID { get; set; }
        public Nullable<int> Turno_ID { get; set; }
        public Nullable<int> Zona_ID { get; set; }
        public Nullable<int> Forma_ID { get; set; }
        public Nullable<int> Supervisor_ID { get; set; }
        public Nullable<int> Cuenta_Contable_ID { get; set; }
        public Nullable<int> Motivo_Baja_ID { get; set; }
        public Nullable<int> Tipo_Regimen_ID { get; set; }
        public Nullable<int> Tipo_Riesgo_ID { get; set; }
        public Nullable<int> Departamento_ID { get; set; }
        public Nullable<int> Periodicidad_ID { get; set; }
        public string Min_Tol_Falta { get; set; }
        public string Min_Tol_Retardo { get; set; }
        public string Min_Tol_Salida_Anticipada { get; set; }
        public string Dia_Descanso { get; set; }
        public string Secuencia_Turnos_ID { get; set; }
        public string Fecha_Baja_IMSS { get; set; }

        public string Nombre_Cuenta_Contable { get; set; }
    }
}