using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeWorld.Models
{
    public class Cook
    {
        public int CookId { get; set; }
        public string CookFname { get; set; }
        public string CookLname { get; set; }
        public string CookEmail { get; set; }
        public string CookPhone { get; set; }
        public int CookExperience { get; set; }
        public string CookSpeciality { get; set; }
    }
}