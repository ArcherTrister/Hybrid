// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 12:44</last-date>
// -----------------------------------------------------------------------

using IdentityModel.Client;

using IdentityServer4;

using Newtonsoft.Json.Linq;

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Docfx();

            Accuracy();
            await IdentityServer();

        }

        private static void Docfx()
        {
            BuildYml build = new BuildYml();
            Console.WriteLine("###############################################");
            Console.WriteLine("#    SetRe.json文件，可配置程序设置           #");
            Console.WriteLine("#    Dir_Exclude：排除生成的文件夹            #");
            Console.WriteLine("#    toc.yml：排除生成的文件                  #");
            Console.WriteLine("###############################################");
            Console.WriteLine("\n输入要处理的目录：");
            string catalog = Console.ReadLine();

            PrintfInit();

            string b = "";
            while (true)
            {
                b = Console.ReadLine();
                if ((new string[] { "1", "2", "3" }).Contains(b))
                    break;
                Console.WriteLine("输入无效，请重新输入");
                PrintfInit();
            }

            while (true)
            {
                Console.WriteLine("生成名称是否需要去除目录或文件前的序号：\n输入(y/n)");
                string vc = Console.ReadLine();
                if (vc.ToLower() == "y")
                {
                    isSpilt = true;
                    break;
                }
                else if (vc.ToLower() == "n")
                {
                    break;
                }
                Console.WriteLine("输入无效，请重新输入");
            }
            if (isSpilt == true)
                while (true)
                {

                    Console.WriteLine("是否需要修改文件(去除目录或文件的序号)：\n输入(y/n)");
                    string vc = Console.ReadLine();
                    if (vc.ToLower() == "y")
                    {
                        isSpiltAll = true;
                        break;
                    }
                    else if (vc.ToLower() == "n")
                    {
                        break;
                    }
                    Console.WriteLine("输入无效，请重新输入");
                }
            if (isSpilt == true)
                Console.WriteLine("输入序号分隔符：");
            Spilt = Console.ReadLine();



            switch (b)
            {
                case "1": build.homewhat = 1; break;
                case "2": build.homewhat = 2; build.homepagename = "index.md"; break;
                case "3": build.homewhat = 3; build.homepagename = b; break;
                default: return;
            }
            Console.WriteLine("处理结果：" + build.Builder(catalog));
            Console.WriteLine("输入 d 可以删除本次生成的文件");
            if (Console.ReadLine().ToLower() == "d")
            {
                build.Dedocyml();
            }
            Console.ReadKey();
        }

        public static bool isSpilt = false;
        public static string Spilt = " ";
        public static bool isSpiltAll = false;
        private static void PrintfInit()
        {
            Console.WriteLine("指定每个目录要生成的默认首页规则：\n1.默认生成方式(输入序号 1) \n" +
                "2. 每个目录以 index.md 为首页(输入序号 2)\n" +
                "3. 其他规则请直接输入文件名(如 home.md)");
        }

        private static void Accuracy()
        {
            int a = 3;
            double b = 0.6;
            Console.WriteLine(a + b);

            Console.WriteLine(a / b);

            Console.WriteLine(a * b);
            Console.WriteLine(a - b);
            Console.ReadKey();
        }

        private static async Task IdentityServer()
        {
            // discovery endpoint
            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5002/");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request access token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "consoleClient",
                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",
                Scope = IdentityServerConstants.LocalApi.ScopeName
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            // call Identity Resource API
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response = await apiClient.GetAsync("http://localhost:5002/api/test/get/");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
            Console.ReadKey();
        }
    }
}