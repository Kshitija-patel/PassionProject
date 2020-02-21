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
    public class CommentController : Controller
    {
        private RecipeContext db = new RecipeContext();

        [HttpPost]
        public ActionResult Add(int id, string CommentContent, int CookId)
        {
            string query = "insert into Comments (CommentContent, CookId, RecipeID) values (@CommentContent, @CookId, @RecipeID)";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@CommentContent", CommentContent);
            sqlparams[1] = new SqlParameter("@CookId", CookId);
            sqlparams[2] = new SqlParameter("@RecipeID", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);


            return RedirectToAction("List", "Recipe");
        }


        public ActionResult Add(int id)
        {
            AddComments viewmodel = new AddComments();

            List<Cook> cook = db.Cooks.SqlQuery("select * from cooks").ToList();
            List<Recipe> recipes = db.Recipes.SqlQuery("select * from recipes").ToList();
            foreach (var recipe in recipes)
            {
                if(recipe.RecipeID == id)
                {
                    viewmodel.recipe = recipe;
                }
            }
            viewmodel.recipes = recipes;
            viewmodel.cook = cook;

            return View(viewmodel);
        }
        public ActionResult Update(int id)
        {
            Comment selectedcomment = db.Comments.SqlQuery("Select * from comments where CommentID=@CommentID", new SqlParameter("@CommentID", id)).FirstOrDefault();
            List<Cook> cook = db.Cooks.SqlQuery("Select * from cooks").ToList();
            List<Recipe> recipes = db.Recipes.SqlQuery("select * from recipes").ToList();
            UpdateComments viewmodel = new UpdateComments();
            viewmodel.comment = selectedcomment;
            viewmodel.cooks = cook;
            viewmodel.recipes = recipes;
            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Update(int id, string CommentContent, int CookId, int RecipeId)
        {
            string query = "Update comments set CommentContent=@CommentContent, CookId=@CookId, RecipeID=@RecipeID where CommentID=@CommentID";
            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@CommentContent", CommentContent);
            sqlparams[1] = new SqlParameter("@CookId", CookId);
            sqlparams[2] = new SqlParameter("@RecipeID", RecipeId);
            sqlparams[3] = new SqlParameter("@CommentID", id);


            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List", "Recipe");
        }
        public ActionResult ConfirmDelete(int id)
        {
            string query = "delete from comments where commentid = @id";
            SqlParameter sqlparams = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List","Recipe");

        }
        public ActionResult Delete(int id)
        {
            string query = "select * from comments where commentid=@id";
            SqlParameter sqlparams = new SqlParameter("@id", id);

            Comment selectedcomment = db.Comments.SqlQuery(query, sqlparams).FirstOrDefault();

            return View(selectedcomment);
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