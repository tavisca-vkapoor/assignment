using GitHubTestApi.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubTestApi.Helper
{
    public class DataHelper
    {
        public static string PopulateJsonDataForNewPublicRepository(string repositoryName,String description)
        {
            CreateRepository createRepository = new CreateRepository();
            createRepository.description = description;
            createRepository.name = repositoryName;
            createRepository.has_issues = true;
            createRepository.homepage = "https://github.com";
            createRepository.has_projects = true;
            createRepository.has_wiki = true;

            return JsonConvert.SerializeObject(createRepository);
        }
    }
}
