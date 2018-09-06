namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblEmployee_EmployeeId_Modified_to_string : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Requests", new[] { "EmployeeId" });
            AlterColumn("dbo.Requests", "EmployeeId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Requests", "EmployeeId", c => c.Int());
            CreateIndex("dbo.Requests", "EmployeeId");
            AddForeignKey("dbo.Requests", "EmployeeId", "dbo.Employees", "Id");
        }
    }
}
