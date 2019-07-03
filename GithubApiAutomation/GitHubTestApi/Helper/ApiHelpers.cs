using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GitHubTestApi.Helper
{

    class ApiHelpers
    {
        public static HttpResponseMessage Get(string path, HttpClient httpClient)
        {
            var response = httpClient.GetAsync(path).GetAwaiter().GetResult();
            return response;
        }

        public static HttpResponseMessage PostAsJsonAsync(string path, HttpClient httpClient, HttpContent content)
        {
            var response = httpClient.PostAsync(path, content).GetAwaiter().GetResult();
            return response;
        }

        public static HttpResponseMessage Put(string path,HttpClient httpClient,HttpContent content)
        {
            var response = httpClient.PutAsync(path, content).GetAwaiter().GetResult();
            return response;
        }

        public static ApiResponse<T> ResponseMapper<T>(HttpResponseMessage httpResponseMessage)
        {
            var value = httpResponseMessage.Content.ReadAsStringAsync();

            var result = new ApiResponse<T>
            {
                StatusCode = httpResponseMessage.StatusCode,
                ResultAsString = value.Result,
            };

            try
            {
                result.Result = JsonConvert.DeserializeObject<T>(value.Result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }
    }
}
