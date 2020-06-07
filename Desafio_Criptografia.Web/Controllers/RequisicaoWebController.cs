using Desafio_Criptografia.Core.Enums;
using Desafio_Criptografia.Core.Services;
using Desafio_Criptografia.Core.ViewModels;
using Desafio_Criptografia.Web.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Desafio_Criptografia.Web.Controllers
{
    public class RequisicaoWebController
    {
        private readonly string _urlRequisicaoGet;
        private readonly string _urlRequisicaoPost;
        private readonly string _token;

        public RequisicaoWebController(string token, string urlRequisicaoGet, string urlRequisicaoPost)
        {
            _token = token;
            _urlRequisicaoGet = urlRequisicaoGet;
            _urlRequisicaoPost = urlRequisicaoPost;
        }

        public string GetDadosJson()
        {
            var json = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_urlRequisicaoGet + _token);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            return json;
        }

        public async void PostResultJson(string filepath)
        {
            var url = _urlRequisicaoPost + _token;
            var fyleArray = File.ReadAllBytes(filepath);
            var filename = "answer.json";
            var contentType = "multipart/form-data";

            UploadMultipart(fyleArray, filename, contentType, url);
        }

        public void UploadMultipart(byte[] file, string filename, string contentType, string url)
        {
            var webClient = new WebClient();
            string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
            webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
            var fileData = webClient.Encoding.GetString(file);
            var package = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"answer\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", boundary, filename, contentType, fileData);

            var nfile = webClient.Encoding.GetBytes(package);

            byte[] resp = webClient.UploadData(url, "POST", nfile);
        }
    }
}
