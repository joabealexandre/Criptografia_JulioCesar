using Desafio_Criptografia.Core.Controllers;
using Desafio_Criptografia.Core.Criptografias;
using Desafio_Criptografia.Core.Enums;
using Desafio_Criptografia.Core.Services;
using Desafio_Criptografia.Core.ViewModels;
using Desafio_Criptografia.Web.Controllers;
using Desafio_Criptografia.Web.Core.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Desafio_Criptografia
{
    public class Program
    {
        private static IConfigurationRoot Configuration { get; set; }
        private static string pathJson;
        private static string _token;
        private static RequisicaoWebController requisicaoWeb;

        public async static Task Main(string[] args)
        {
            SetConfiguration();
            await MainAsync(args);
        }

        public static async Task MainAsync(string[] args)
        {
            ApresentaMenu();
        }

        /// <summary>
        /// Faz a requisição GET para obter o arquivo com o texto a ser decifrado
        /// </summary>
        private static void GetArquivoJsonAPI()
        {
            var json = requisicaoWeb.GetDadosJson();
            AtualizarArquivoJson(json);
        }

        /// <summary>
        /// Decifra o texto do arquivo JSON e atualiza o mesmo com o texto decifrado
        /// </summary>
        private static void DecifrarTextoJson()
        {
            var requisicaoJson = JsonConvert.DeserializeObject<RequisicaoJson>(File.ReadAllText(pathJson));

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
            requisicaoJson.token = _token;

            AtualizarArquivoJson(JsonConvert.SerializeObject(requisicaoJson, Formatting.Indented));

            Console.WriteLine("***** RESULTADO *****");
            Console.WriteLine($"Texto cifrado: {requisicaoJson.cifrado}");
            Console.WriteLine($"Texto decifrado: {requisicaoJson.decifrado}");
            Console.WriteLine($"Fator de substituição: {requisicaoJson.numero_casas}");

            Console.WriteLine("\n\nPressione qualquer tecla para continuar");
            Console.ReadLine();
        }

        /// <summary>
        /// Enviar o arquivo JSON para a API
        /// </summary>
        private static void EnviarArquivoJsonAPI()
        {
            requisicaoWeb.PostResultJson(pathJson);
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

        public static void ApresentaMenu()
        {
            string opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("Selecione uma opção:");
                Console.WriteLine("1 - Download arquivo JSON API (GET)");
                Console.WriteLine("2 - Decifrar texto - Atualizar JSON");
                Console.WriteLine("3 - Enviar arquivo JSON (POST)");
                Console.WriteLine("0 - Sair");
                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        GetArquivoJsonAPI();
                        Console.WriteLine("O download do arquivo JSON foi realizado com sucesso!\n\n");
                        break;
                    case "2":
                        DecifrarTextoJson();
                        break;
                    case "3":
                        EnviarArquivoJsonAPI();
                        break;
                    default:
                        continue;
                }

            } while (opcao != "0");
        }

        /// <summary>
        /// Cria um objeto IConfigurationRoot baseado no arquivo appsettings.json e preenche as campos estáticos da classe
        /// </summary>
        private static void SetConfiguration()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            pathJson = Configuration.GetSection("filePath").Value;
            _token = Configuration.GetSection("token").Value;
            requisicaoWeb = new RequisicaoWebController(_token, Configuration.GetSection("urls")["GET"], Configuration.GetSection("urls")["POST"]);
        }
    }
}
