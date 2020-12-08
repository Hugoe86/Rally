using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace admin_trazabilidad.Models.Ayudante
{
    public class Cls_File_Provider
    {
        #region Methods

        #region Get Multipart Provider

        public static MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = Cls_Config.FolderUploads;
            var root = HttpContext.Current.Server.MapPath(uploadFolder);

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            return new MultipartFormDataStreamProvider(root);
        }

        #endregion

        #region Get DeserializedFileName

        public static string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);

            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        #endregion

        #region Get File Name

        public static string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

        #endregion

        #endregion
    }
}