using admin_trazabilidad.Models.Ayudante;
using admin_trazabilidad.Models.Negocio;
using datos_trazabilidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace admin_trazabilidad.Paginas.Paginas_Generales.controllers
{
    [RoutePrefix("api/ImagenEmpresa")]
    public class ImagenEmpresaApiController : ApiController
    {
        [HttpPost]
        [Route("GuardarImagenEmpresa")]
        public async Task<bool> GuardarImagenEmpresa()
        {
            bool result = false;

            if (!Request.Content.IsMimeMultipartContent())
            {
                result = false;
            }
            else
            {
                try
                {
                    var provider = Cls_File_Provider.GetMultipartProvider();
                    var multipart = await Request.Content.ReadAsMultipartAsync(provider);
                    var empresaId = multipart.FormData.Get("Empresa_ID");
                    Cls_Apl_Cat_Empresas_Negocio empresa = new Cls_Apl_Cat_Empresas_Negocio()
                    {
                        Empresa_ID = int.Parse(empresaId),
                        Ruta_Imagen = System.IO.File.ReadAllBytes(multipart.FileData[0].LocalFileName),
                    };

                    result = await ImagenEmpresaApiController.guardarImagenEmpresa(empresa);
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
        }

        private static async Task<bool> guardarImagenEmpresa(Cls_Apl_Cat_Empresas_Negocio entity)
        {
            bool result = false;

            try
            {
                using (Sistema_TrazabilidadEntities context = new Sistema_TrazabilidadEntities())
                {
                    var empresa = context.Apl_Empresas.Where(x => x.Empresa_ID == entity.Empresa_ID).First();
                    empresa.Ruta_Imagen = entity.Ruta_Imagen;
                    context.SaveChanges();
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}
