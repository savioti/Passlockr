namespace Passlockr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoLoginAoModelo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DadosSenhas", "Login", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DadosSenhas", "Login");
        }
    }
}
