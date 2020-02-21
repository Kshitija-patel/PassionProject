using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecipeWorld.Data
{
    public class RecipeContext : DbContext
    {
        public RecipeContext() : base("name=RecipeContext")
        {
        }

        public System.Data.Entity.DbSet<RecipeWorld.Models.Cook> Cooks { get; set; }
        public System.Data.Entity.DbSet<RecipeWorld.Models.Recipe> Recipes { get; set; }
        public System.Data.Entity.DbSet<RecipeWorld.Models.Comment> Comments { get; set; }
    }
}