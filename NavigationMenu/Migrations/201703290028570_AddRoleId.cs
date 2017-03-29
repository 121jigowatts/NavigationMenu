namespace NavigationMenu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MenuItems", "RoleId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MenuItems", "RoleId");
        }
    }
}
