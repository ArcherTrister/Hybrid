// -----------------------------------------------------------------------
//  <copyright file="MainWindow.xaml.cs" company="cn.lxking">
//      Copyright © 2019-2020 Hybrid. All rights reserved.
//  </copyright>
//  <site>https://www.lxking.cn</site>
//  <last-editor>ArcherTrister</last-editor>
//  <last-date>2017-09-01 12:44</last-date>
// -----------------------------------------------------------------------

using IdentityModel.Client;

using System.Net.Http;
using System.Windows;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _accessToken;
        private DiscoveryDocumentResponse _disco;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private async void RequestAccessToken_ButtonClick(object sender, RoutedEventArgs e)
        {
            var userName = UserNameInput.Text;
            var password = PasswordInput.Password;

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5002/");
            _disco = disco;
            if (disco.IsError)
            {
                MessageBox.Show(disco.Error);
                return;
            }

            // request access token
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                //ClientId = "wpfClient",
                //ClientSecret = "wpf secrect",
                ClientId = "mobileAppClient",
                ClientSecret = "mobile app secrect",
                Scope = "IdentityServerApi openid profile HybridCustom offline_access",
                //Scope = "IdentityServerApi openid profile",
                UserName = userName,
                Password = password
            });

            if (tokenResponse.IsError)
            {
                MessageBox.Show(tokenResponse.Error);
                return;
            }

            _accessToken = tokenResponse.AccessToken;
            AccessTokenTextBlock.Text = tokenResponse.Json.ToString();

            getIdentity();
        }

        private async void RequestApi1Resource_ButtonClick(object sender, RoutedEventArgs e)
        {
            // call API1 Resource
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(_accessToken);

            var response = await apiClient.GetAsync("http://localhost:5002/api/test/get/");
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Api1ResponseTextBlock.Text = content;
            }
        }

        private async void RequestIdentityResource_ButtonClick(object sender, RoutedEventArgs e)
        {
            // call Identity Resource from Identity Server
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(_accessToken);

            var response = await apiClient.GetAsync(_disco.UserInfoEndpoint);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                IdentityResponseTextBlock.Text = content;
            }
        }

        private async void getIdentity()
        {
            // call Identity Resource from Identity Server
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(_accessToken);

            var response = await apiClient.GetAsync(_disco.UserInfoEndpoint);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(response.StatusCode.ToString());
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                IdentityResponseTextBlock.Text = content;
            }
        }
    }
}