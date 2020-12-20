namespace Passlockr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoDataEdicao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DadosSenhas", "DataEdicao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DadosSenhas", "DataEdicao");
        }
    }
}
