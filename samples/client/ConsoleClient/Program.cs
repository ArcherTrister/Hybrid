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
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            //int a = 3;
            //double b = 0.6;
            //Console.WriteLine(a + b);

            //Console.WriteLine(a / b);

            //Console.WriteLine(a * b);
            //Console.WriteLine(a - b);
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