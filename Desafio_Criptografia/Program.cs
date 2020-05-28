using Desafio_Criptografia.Core.Controllers;
using Desafio_Criptografia.Core.Criptografias;
using Desafio_Criptografia.Core.Enums;
using Desafio_Criptografia.Core.Services;
using Desafio_Criptografia.Core.ViewModels;
using Desafio_Criptografia.Web.Controllers;
using Desafio_Criptografia.Web.Core.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Desafio_Criptografia
{
    public class Program
    {
        private static IConfigurationRoot configuration;
        private static readonly string pathJson = "answer.json";
        private const string TOKEN = "0ac32b8ba008dd4501180cbb2b0a349709068255";


        public async static Task Main(string[] args)
        {
            await MainAsync(args);
        }


        public static async Task MainAsync(string[] args)
        {
            RequisicaoWebController requisicaoWeb = new RequisicaoWebController();
            var json = requisicaoWeb.GetDadosJson();
            AtualizarArquivoJson(json);
            var requisicaoJson = JsonConvert.DeserializeObject<RequisicaoJson>(json);

            AlfabetoService alfabetoService = new AlfabetoService();
            JulioCesarCriptografia jcCriptografia = new JulioCesarCriptografia(alfabetoService, requisicaoJson.numero_casas);
            CriptografiaController criptografiaController = new CriptografiaController(jcCriptografia);
            ObjetoCriptografia obj = new ObjetoCriptografia
            {
                Fator = requisicaoJson.numero_casas,
                Texto = requisicaoJson.cifrado,
                Operacao = EOperacao.DESCRIPTOGRAFAR
            };

            var criptoResultado = criptografiaController.Descriptografar(obj);
            requisicaoJson.decifrado = criptoResultado.Resultado;
            requisicaoJson.resumo_criptografico = criptoResultado.Hash;
            requisicaoJson.token = TOKEN;

            json = JsonConvert.SerializeObject(requisicaoJson, Formatting.Indented);
            AtualizarArquivoJson(json);

            requisicaoWeb.PostResultJson(json, pathJson);
        }

        /// <summary>
        /// Atualiza o conteúdo do arquivo answer.json
        /// </summary>
        /// <param name="json">Conteúdo do JSON</param>
        /// <returns></returns>
        public static void AtualizarArquivoJson(string json)
        {
            using (var sw = new StreamWriter(pathJson, false))
            {
                sw.Write(json);
            }
        }

        public static void ConfigureServices(IServiceCollection serviceCollection)
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
        }
    }
}
