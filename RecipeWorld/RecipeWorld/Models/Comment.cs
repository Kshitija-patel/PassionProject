using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeWorld.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        public string CommentContent { get; set; }
        public int RecipeID { get; set; }
        [ForeignKey("RecipeID")]
        public virtual Recipe Recipes { get; set; }

        public int? CookID { get; set; }
        [ForeignKey("CookID")]
        public virtual Cook Cooks { get; set; }
    }
}