using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeWorld.Models.ViewModels
{
    public class ShowComments
    {
        public Recipe recipe { get; set; }
        public List<Comment> comments { get; set; }
    }
}