namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblEmployee_Modified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ImagePath", c => c.String());
            AddColumn("dbo.Employees", "IsDriver", c => c.Boolean(nullable: false));
            AddColumn("dbo.Employees", "DrivingLicenseNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "DrivingLicenseNo");
            DropColumn("dbo.Employees", "IsDriver");
            DropColumn("dbo.Employees", "ImagePath");
        }
    }
}
