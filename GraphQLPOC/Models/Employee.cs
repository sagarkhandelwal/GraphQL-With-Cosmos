using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLPOC.Models
{
    public class Employee
    {
        [Key]
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Designation { get; set; }

        public IList<Company> PreviousCompanies { get; set; }
    }

    public class Company
    {
        public string CompanyName { get; set; }

    }
}
