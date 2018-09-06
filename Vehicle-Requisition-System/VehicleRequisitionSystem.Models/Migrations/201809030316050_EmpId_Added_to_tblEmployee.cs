namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpId_Added_to_tblEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "EmpIdNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "EmpIdNo");
        }
    }
}
