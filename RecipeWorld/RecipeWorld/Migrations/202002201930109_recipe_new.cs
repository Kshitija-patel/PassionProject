namespace RecipeWorld.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class recipe_new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "RecipeProcedure", c => c.String());
            DropColumn("dbo.Recipes", "RecipeProdecure");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "RecipeProdecure", c => c.String());
            DropColumn("dbo.Recipes", "RecipeProcedure");
        }
    }
}
