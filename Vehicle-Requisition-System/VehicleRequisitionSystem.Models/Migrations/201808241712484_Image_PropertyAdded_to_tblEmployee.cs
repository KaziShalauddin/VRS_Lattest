namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Image_PropertyAdded_to_tblEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Image");
        }
    }
}
