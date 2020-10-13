namespace BoilerplatePro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPasswordResetUserFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpUsers", "PasswordResetExpiry", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpUsers", "PasswordResetExpiry");
        }
    }
}
