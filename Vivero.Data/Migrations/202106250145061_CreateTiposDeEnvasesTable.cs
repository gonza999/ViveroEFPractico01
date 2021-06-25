namespace Vivero.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTiposDeEnvasesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TiposDeEnvases",
                c => new
                    {
                        TipoDeEnvaseId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TipoDeEnvaseId);
            
            Sql("Insert into TiposDeEnvases Values('Maseta chica')");
            Sql("Insert into TiposDeEnvases Values('Maseta mediana')");
            Sql("Insert into TiposDeEnvases Values('Maseta grande')");
            Sql("Insert into TiposDeEnvases Values('Envase Plastico')");
            Sql("Insert into TiposDeEnvases Values('En Tierra')");
        }
        
        public override void Down()
        {
            DropTable("dbo.TiposDeEnvases");
        }
    }
}
