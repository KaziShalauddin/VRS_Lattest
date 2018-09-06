namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblRequest_stringEmployeeId_Modified_to_EmpIdNo_And_tblEmployee_Association_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Requests", "EmpIdNo", c => c.String());
            AlterColumn("dbo.Requests", "EmployeeId", c => c.Int());
            CreateIndex("dbo.Requests", "EmployeeId");
            AddForeignKey("dbo.Requests", "EmployeeId", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Requests", new[] { "EmployeeId" });
            AlterColumn("dbo.Requests", "EmployeeId", c => c.String());
            DropColumn("dbo.Requests", "EmpIdNo");
        }
    }
}
