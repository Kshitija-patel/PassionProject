using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipeWorld.Models.ViewModels
{
    public class UpdateRecipe
    {
            //Information needed
            //Info about one pet
            //Info about many species

            public Recipe recipe { get; set; }
            public List<Cook> cook { get; set; }
     }
    
}