namespace VehicleRequisitionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionRequestTable_Modified : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RequisitionRequests", name: "Employee_Id", newName: "EmployeeId");
            RenameIndex(table: "dbo.RequisitionRequests", name: "IX_Employee_Id", newName: "IX_EmployeeId");
            AddColumn("dbo.RequisitionRequests", "Place", c => c.String());
            AddColumn("dbo.RequisitionRequests", "IsAssigned", c => c.Boolean(nullable: false));
            AddColumn("dbo.RequisitionRequests", "VehicleId", c => c.Int());
            AddColumn("dbo.RequisitionRequests", "DriverId", c => c.Int());
            CreateIndex("dbo.RequisitionRequests", "VehicleId");
            CreateIndex("dbo.RequisitionRequests", "DriverId");
            AddForeignKey("dbo.RequisitionRequests", "DriverId", "dbo.Drivers", "Id");
            AddForeignKey("dbo.RequisitionRequests", "VehicleId", "dbo.Vehicles", "Id");
            DropColumn("dbo.RequisitionRequests", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequisitionRequests", "Status", c => c.String());
            DropForeignKey("dbo.RequisitionRequests", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.RequisitionRequests", "DriverId", "dbo.Drivers");
            DropIndex("dbo.RequisitionRequests", new[] { "DriverId" });
            DropIndex("dbo.RequisitionRequests", new[] { "VehicleId" });
            DropColumn("dbo.RequisitionRequests", "DriverId");
            DropColumn("dbo.RequisitionRequests", "VehicleId");
            DropColumn("dbo.RequisitionRequests", "IsAssigned");
            DropColumn("dbo.RequisitionRequests", "Place");
            RenameIndex(table: "dbo.RequisitionRequests", name: "IX_EmployeeId", newName: "IX_Employee_Id");
            RenameColumn(table: "dbo.RequisitionRequests", name: "EmployeeId", newName: "Employee_Id");
        }
    }
}
