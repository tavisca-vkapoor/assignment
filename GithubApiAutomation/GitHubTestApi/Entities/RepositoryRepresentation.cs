using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubTestApi.Entities
{
    public class RepositoryRepresentation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }

    }
}
