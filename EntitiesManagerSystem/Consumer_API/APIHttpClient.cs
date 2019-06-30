using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using EntitiesManagerSystem.Models;
using Newtonsoft.Json.Linq;

namespace EntitiesManagerSystem.Consumers_API
{
    public class APIHttpClient
    {
        private string baseAPI = "http://localhost:5002/api/";
        public APIHttpClient(string baseAPI)
        {
            this.baseAPI = baseAPI;
        }

        public Guid Put<T>(string action, Guid id, T data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAPI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PutAsJsonAsync(action + id.ToString(), data).Result;
                if (response.IsSuccessStatusCode)
                {
                    var sucesso = response.Content.ReadAsAsync<Guid>().Result;
                    return sucesso;
                }
                else
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            }
        }

        public Guid Post<T>(string action, T data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAPI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.PostAsJsonAsync(action, data).Result;
                if (response.IsSuccessStatusCode)
                {
                    var sucesso = response.Content.ReadAsAsync<Guid>().Result;
                    return sucesso;
                }
                else
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            }
        }

        public T Get<T>(string actionUri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAPI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync(actionUri).Result;
                if (response.IsSuccessStatusCode)
                {
                    T sucesso = response.Content.ReadAsAsync<T>().Result;
                    return sucesso;
                }
                else
                {
                    //Pode-se registrar as falhas neste local
                    //joga para o cliente a falha
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            }
        }

        public T Delete<T>(string action, Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAPI);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.DeleteAsync(action + id.ToString()).Result;
                if (response.IsSuccessStatusCode)
                {
                    var sucesso = response.Content.ReadAsAsync<T>().Result;
                    return sucesso;
                }
                else
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            }
        }
        
        
        public string AuthenticationPost(string email, string password)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAPI);
                client.DefaultRequestHeaders.Accept.Clear();
                var request = new HttpRequestMessage(HttpMethod.Post, (this.baseAPI + "token"));
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                    { "username", email},
                    { "password", password },
                    { "grant_type", "password" }
                });

                var response = client.SendAsync(request).Result;


                if (response.IsSuccessStatusCode)
                {
                    var payload = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    var token = payload.Value<string>("access_token");

                    //Salva o usuario, senha e token em memoria
                    User user = new User()
                    {
                        Password = password,
                        Email = email,
                        Token = token
                    };
                    HttpContext.Current.Session["user"] = user;

                    return token;
                }
                else
                {
                    throw new Exception(response.Content.ReadAsStringAsync().Result);
                }
            }
        }

        private void SetarParametrosAutenticacao(HttpClient httpClient)
        {
            if (HttpContext.Current.Session["user"] != null)
            {
                var user = (User)HttpContext.Current.Session["user"];

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
            }
        }
        
    }
}