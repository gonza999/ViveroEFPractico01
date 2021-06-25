namespace Vivero.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRelationPlantasEnvases : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plantas", "TipoDeEnvaseId", c => c.Int(nullable: false));
            Sql("UPDATE Plantas SET TipoDeEnvaseId=5");
            CreateIndex("dbo.Plantas", "TipoDeEnvaseId");
            AddForeignKey("dbo.Plantas", "TipoDeEnvaseId", "dbo.TiposDeEnvases", "TipoDeEnvaseId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Plantas", "TipoDeEnvaseId", "dbo.TiposDeEnvases");
            DropIndex("dbo.Plantas", new[] { "TipoDeEnvaseId" });
            DropColumn("dbo.Plantas", "TipoDeEnvaseId");
        }
    }
}
