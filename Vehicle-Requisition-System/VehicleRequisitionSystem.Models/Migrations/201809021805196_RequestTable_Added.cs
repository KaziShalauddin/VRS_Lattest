namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestTable_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
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
                        ConfigurationId = c.Int(),
                        AssignedRequisitionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AssignedRequisitions", t => t.AssignedRequisitionId)
                .ForeignKey("dbo.Configurations", t => t.ConfigurationId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.ConfigurationId)
                .Index(t => t.AssignedRequisitionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Requests", "ConfigurationId", "dbo.Configurations");
            DropForeignKey("dbo.Requests", "AssignedRequisitionId", "dbo.AssignedRequisitions");
            DropIndex("dbo.Requests", new[] { "AssignedRequisitionId" });
            DropIndex("dbo.Requests", new[] { "ConfigurationId" });
            DropIndex("dbo.Requests", new[] { "EmployeeId" });
            DropTable("dbo.Requests");
        }
    }
}
