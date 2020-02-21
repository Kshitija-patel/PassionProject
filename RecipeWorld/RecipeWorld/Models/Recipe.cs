using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWorld.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }
        public string RecipeTitle { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeIngredients { get; set; }
        public string RecipeProcedure { get; set; }
        public string RecipeCookTime { get; set; }
        public int RecipeServes { get; set; }
        public int CookID { get; set; }
        [ForeignKey("CookID")]
        public virtual Cook Cooks { get; set; }

    }
}