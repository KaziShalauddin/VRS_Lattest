namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedRequisitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        RequisitionRequestId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.RequisitionRequests", t => t.RequisitionRequestId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.RequisitionRequestId)
                .Index(t => t.DriverId)
                .Index(t => t.VehicleId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DepartureTime = c.DateTime(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        Status = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        UserId = c.String(),
                        DepartmentId = c.Int(),
                        DesignationId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Designations", t => t.DesignationId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.DesignationId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Designations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequisitionRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Description = c.String(),
                        Location = c.String(),
                        Persons = c.Int(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        CheckInTime = c.DateTime(nullable: false),
                        IsCanceled = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        EmployeeId = c.Int(),
                        StatusId = c.Int(),
                        AssignedRequisitionId = c.Int(),
                        AssignedRequisition_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssignedRequisitions", t => t.AssignedRequisitionId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .ForeignKey("dbo.AssignedRequisitions", t => t.AssignedRequisition_Id)
                .Index(t => t.EmployeeId)
                .Index(t => t.StatusId)
                .Index(t => t.AssignedRequisitionId)
                .Index(t => t.AssignedRequisition_Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        Model = c.String(),
                        RegistrationNo = c.String(),
                        ChesisNo = c.String(),
                        Capacity = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Configurations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ConfigurationTypeId = c.Int(nullable: false),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConfigurationTypes", t => t.ConfigurationTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.ConfigurationTypeId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.ConfigurationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Configurations", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Configurations", "ConfigurationTypeId", "dbo.ConfigurationTypes");
            DropForeignKey("dbo.AssignedRequisitions", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.RequisitionRequests", "AssignedRequisition_Id", "dbo.AssignedRequisitions");
            DropForeignKey("dbo.AssignedRequisitions", "RequisitionRequestId", "dbo.RequisitionRequests");
            DropForeignKey("dbo.AssignedRequisitions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.RequisitionRequests", "StatusId", "dbo.Status");
            DropForeignKey("dbo.RequisitionRequests", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.RequisitionRequests", "AssignedRequisitionId", "dbo.AssignedRequisitions");
            DropForeignKey("dbo.Employees", "DesignationId", "dbo.Designations");
            DropForeignKey("dbo.Departments", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.AssignedRequisitions", "DriverId", "dbo.Drivers");
            DropIndex("dbo.Configurations", new[] { "OrganizationId" });
            DropIndex("dbo.Configurations", new[] { "ConfigurationTypeId" });
            DropIndex("dbo.RequisitionRequests", new[] { "AssignedRequisition_Id" });
            DropIndex("dbo.RequisitionRequests", new[] { "AssignedRequisitionId" });
            DropIndex("dbo.RequisitionRequests", new[] { "StatusId" });
            DropIndex("dbo.RequisitionRequests", new[] { "EmployeeId" });
            DropIndex("dbo.Departments", new[] { "OrganizationId" });
            DropIndex("dbo.Employees", new[] { "DesignationId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "VehicleId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "DriverId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "RequisitionRequestId" });
            DropIndex("dbo.AssignedRequisitions", new[] { "EmployeeId" });
            DropTable("dbo.ConfigurationTypes");
            DropTable("dbo.Configurations");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Status");
            DropTable("dbo.RequisitionRequests");
            DropTable("dbo.Designations");
            DropTable("dbo.Organizations");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.Drivers");
            DropTable("dbo.AssignedRequisitions");
        }
    }
}
