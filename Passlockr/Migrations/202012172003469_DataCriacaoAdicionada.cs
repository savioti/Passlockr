namespace Passlockr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataCriacaoAdicionada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DadosSenhas", "DataCriacao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DadosSenhas", "DataCriacao");
        }
    }
}
