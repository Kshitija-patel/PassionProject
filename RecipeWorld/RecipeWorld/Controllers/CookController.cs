using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.SqlClient;
using System.Web.Mvc;
using RecipeWorld.Data;
using RecipeWorld.Models;
using System.Diagnostics;

namespace RecipeWorld.Controllers
{
    public class CookController : Controller
    {
        private RecipeContext db = new RecipeContext();

        public ActionResult List()
        {
            List<Cook> cooks = db.Cooks.SqlQuery("Select * from Cooks").ToList();
            return View(cooks);
        }

        [HttpPost]
        public ActionResult Add(string CookFname,string CookLname,string CookEmail,int CookPhone,int CookExperience,string CookSpeciality)
        {
            string query = "insert into Cooks (CookFname, CookLname, CookEmail, CookPhone, CookExperience, CookSpeciality) values (@CookFname, @CookLname, @CookEmail, @CookPhone, @CookExperience, @CookSpeciality)";
            SqlParameter[] sqlparams = new SqlParameter[6]; 
            sqlparams[0] = new SqlParameter("@CookFname", CookFname);
            sqlparams[1] = new SqlParameter("@CookLname", CookLname);
            sqlparams[2] = new SqlParameter("@CookEmail", CookEmail);
            sqlparams[3] = new SqlParameter("@CookPhone", CookPhone);
            sqlparams[4] = new SqlParameter("@CookExperience", CookExperience);
            sqlparams[5] = new SqlParameter("@CookSpeciality", CookSpeciality);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }


        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Show(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cook cook = db.Cooks.SqlQuery("Select * from cooks where cookid=@CookId", new SqlParameter("@CookId", id)).FirstOrDefault();

            if(cook == null)
            {
                return HttpNotFound();
            }
            return View(cook);
        }
        public ActionResult Update(int id)
        {
            Cook selectedcook = db.Cooks.SqlQuery("Select * from cooks where cookid=@id", new SqlParameter("@id", id)).FirstOrDefault();
        
            return View(selectedcook);
        }
        //[HttpPost] Update

        [HttpPost]
        public ActionResult Update(int id, string CookFname, string CookLname, string CookEmail, int CookPhone, int CookExperience, string CookSpeciality)
        {
            string query = "Update cooks set CookFname=@CookFname,CookLname=@CookLname, CookEmail=@CookEmail, CookPhone=@CookPhone, CookExperience=@CookExperience, CookSpeciality=@CookSpeciality where CookId=@CookId";
            SqlParameter[] sqlparams = new SqlParameter[7];
            sqlparams[0] = new SqlParameter("@CookFname", CookFname);
            sqlparams[1] = new SqlParameter("@CookLname", CookLname);
            sqlparams[2] = new SqlParameter("@CookEmail", CookEmail);
            sqlparams[3] = new SqlParameter("@CookPhone", CookPhone);
            sqlparams[4] = new SqlParameter("@CookExperience", CookExperience);
            sqlparams[5] = new SqlParameter("@CookSpeciality", CookSpeciality);
            sqlparams[6] = new SqlParameter("@CookId", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult ConfirmDelete(int id)
        {
            string query = "delete from cooks where cookid=@id";
            SqlParameter sqlparams = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            string query = "select * from cooks where cookid = @id";
            SqlParameter sqlparams = new SqlParameter("@id", id);
            Cook selectedcook = db.Cooks.SqlQuery(query, sqlparams).FirstOrDefault();

            return View(selectedcook);
        }
    }
}

//Reference-https://github.com/christinebittle/PetGroomingMVC
