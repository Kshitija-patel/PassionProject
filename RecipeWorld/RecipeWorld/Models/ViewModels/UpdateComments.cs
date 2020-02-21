using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeWorld.Models.ViewModels
{
    public class UpdateComments
    {
        public Comment comment { get; set; }
        public List<Cook> cooks { get; set; }
        public List<Recipe> recipes { get; set; }
    }
}