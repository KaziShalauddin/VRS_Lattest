namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblAssignedRequest_DriverIdRemoved_EmployeeIdAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AssignedRequests", "DriverId", "dbo.Drivers");
            DropIndex("dbo.AssignedRequests", new[] { "DriverId" });
            AddColumn("dbo.AssignedRequests", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.AssignedRequests", "EmployeeId");
            AddForeignKey("dbo.AssignedRequests", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
            DropColumn("dbo.AssignedRequests", "DriverId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AssignedRequests", "DriverId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AssignedRequests", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.AssignedRequests", new[] { "EmployeeId" });
            DropColumn("dbo.AssignedRequests", "EmployeeId");
            CreateIndex("dbo.AssignedRequests", "DriverId");
            AddForeignKey("dbo.AssignedRequests", "DriverId", "dbo.Drivers", "Id", cascadeDelete: true);
        }
    }
}
