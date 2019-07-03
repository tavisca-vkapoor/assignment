using GitHubTestApi.Entities;
using GitHubTestApi.Tests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace GitHubTestApi.Helper
{
    class RepositoryOperations : Setup
    {
        public ApiResponse<List<RepositoryRepresentation>> GetRepositoryForUser(String username,String personalToken)
        {
            var getAllReposUrl = @"/users/"+username+"/repos?access_token="+personalToken+"";
            var response = ApiHelpers.Get(getAllReposUrl, client);
            return ApiHelpers.ResponseMapper<List<RepositoryRepresentation>>(response);
        }

        public ApiResponse<RepositoryRepresentation> CreateNewRepository(String username,String password,String bodyForPost)
        { 
            var stringContent = new StringContent(bodyForPost, Encoding.UTF8, "application/json");
            var byteArray = Encoding.ASCII.GetBytes(""+username+":"+password+"");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = ApiHelpers.PostAsJsonAsync("/user/repos", client, stringContent);
            return ApiHelpers.ResponseMapper<RepositoryRepresentation>(response);
        }

        public HttpResponseMessage StarringRepository(string username,string password,string repoNameToBeStarred)
        {
            var byteArray = Encoding.ASCII.GetBytes("" + username + ":" + password + "");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = ApiHelpers.Put("/user/starred/" + username + "/" + repoNameToBeStarred + "",client,new StringContent(""));
            return response;
        }
        
        public ApiResponse<List<RepositoryRepresentation>> GetStarredRepository(string username,string password)
        {
            var byteArray = Encoding.ASCII.GetBytes("" + username + ":" + password + "");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = ApiHelpers.Get("/user/starred",client);
            return ApiHelpers.ResponseMapper<List<RepositoryRepresentation>>(response);
        }
    }
}
