using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeWorld.Models.ViewModels
{
    public class AddComments
    {
        public Recipe recipe { get; set; }
        public List<Cook> cook { get; set; }
        public List<Recipe> recipes { get; set; }
    }
}