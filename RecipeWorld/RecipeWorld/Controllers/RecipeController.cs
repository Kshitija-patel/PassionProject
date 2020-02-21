using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RecipeWorld.Data;
using RecipeWorld.Models;
using RecipeWorld.Models.ViewModels;
using System.Diagnostics;

namespace RecipeWorld.Controllers
{
    public class RecipeController : Controller
    {
        private RecipeContext db = new RecipeContext();

        public ActionResult List()
        
        {
            List<Recipe> recipes = db.Recipes.SqlQuery("Select * from Recipes").ToList();
            return View(recipes);
        }

        [HttpPost]
        public ActionResult Add(string RecipeTitle, string RecipeDescription, string RecipeCooktime, int RecipeServes, string RecipeIngredients, string RecipeProcedure, int CookID)
        {
            string query = "insert into Recipes (RecipeTitle, RecipeDescription, RecipeCooktime, RecipeServes, RecipeIngredients, RecipeProcedure, CookID) values (@RecipeTitle, @RecipeDescription, @RecipeCooktime, @RecipeServes, @RecipeIngridents, @RecipeProcedure, @CookID)";
            SqlParameter[] sqlparams = new SqlParameter[7];
            sqlparams[0] = new SqlParameter("@RecipeTitle", RecipeTitle);
            sqlparams[1] = new SqlParameter("@RecipeDescription", RecipeDescription);
            sqlparams[2] = new SqlParameter("@RecipeCooktime", RecipeCooktime);
            sqlparams[3] = new SqlParameter("@RecipeServes", RecipeServes);
            sqlparams[4] = new SqlParameter("@RecipeIngridents", RecipeIngredients);
            sqlparams[5] = new SqlParameter("@RecipeProcedure", RecipeProcedure);
            sqlparams[6] = new SqlParameter("@CookID", CookID);
            db.Database.ExecuteSqlCommand(query, sqlparams);


            return RedirectToAction("List");
        }


        public ActionResult Add()
        {
            List<Cook> cook = db.Cooks.SqlQuery("select * from cooks").ToList();

            return View(cook);
        }

        public ActionResult Show(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.SqlQuery("Select * from recipes where recipeid=@RecipeId", new SqlParameter("@RecipeId", id)).FirstOrDefault();

            if (recipe == null)
            {
                return HttpNotFound();
            }
            List<Comment> comments = db.Comments.SqlQuery("Select * from comments where RecipeID=@RecipeId", new SqlParameter("@RecipeId", id)).ToList();
            ShowComments viewmodel = new ShowComments();
            viewmodel.recipe = recipe;
            viewmodel.comments = comments;

            return View(viewmodel);

        }
        public ActionResult Update(int id)
        {
            Recipe selectedrecipe = db.Recipes.SqlQuery("Select * from recipes where recipeid=@id", new SqlParameter("@id", id)).FirstOrDefault();
            List<Cook> cook = db.Cooks.SqlQuery("Select * from cooks").ToList();
            UpdateRecipe viewmodel = new UpdateRecipe();
            viewmodel.recipe = selectedrecipe;
            viewmodel.cook = cook;
            return View(viewmodel);
        }
        //[HttpPost] Update

        [HttpPost]
        public ActionResult Update(int id, string RecipeTitle, string RecipeDescription, string RecipeCooktime, int RecipeServes, string RecipeIngredients, string RecipeProcedure, int CookID)
        {
            string query = "Update recipes set RecipeTitle=@RecipeTitle, RecipeDescription=@RecipeDescription, RecipeIngredients=@RecipeIngredients, RecipeCooktime=@RecipeCooktime, CookID=@CookID, RecipeProcedure=@RecipeProcedure, RecipeServes=@RecipeServes where RecipeID=@RecipeID";
            SqlParameter[] sqlparams = new SqlParameter[8];
            sqlparams[0] = new SqlParameter("@RecipeTitle", RecipeTitle);
            sqlparams[1] = new SqlParameter("@RecipeDescription", RecipeDescription);
            sqlparams[2] = new SqlParameter("@RecipeCooktime", RecipeCooktime);
            sqlparams[3] = new SqlParameter("@RecipeServes", RecipeServes);
            sqlparams[4] = new SqlParameter("@RecipeIngredients", RecipeIngredients);
            sqlparams[5] = new SqlParameter("@RecipeProcedure", RecipeProcedure);
            sqlparams[6] = new SqlParameter("@CookID", CookID);
            sqlparams[7] = new SqlParameter("@RecipeId", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult ConfirmDelete(int id)
        {
            string query = "delete from recipes where recipeid = @id";
            SqlParameter sqlparams = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");

        }
        public ActionResult Delete(int id)
        {
            string query = "select * from recipes where recipeid=@id";
            SqlParameter sqlparams = new SqlParameter("@id", id);

            Recipe selectedrecipe = db.Recipes.SqlQuery(query, sqlparams).FirstOrDefault();

            return View(selectedrecipe);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
//Recipe_Reference-https://food52.com/recipes/31878-creamy-mushroom-pasta