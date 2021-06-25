namespace Vivero.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTipoDePlanta : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into TiposDePlantas Values ('Nuevo tipo de planta')");
        }
        
        public override void Down()
        {
            Sql("Delete from TiposDePlantas where Descripcion='Nuevo tipo de planta'");
        }
    }
}
