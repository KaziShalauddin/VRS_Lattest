namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblAssignedRequest_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EmpIdNo = c.String(),
                        RequestId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Requests", t => t.RequestId, cascadeDelete: true)
                .ForeignKey("dbo.Vehicles", t => t.VehicleId, cascadeDelete: true)
                .Index(t => t.RequestId)
                .Index(t => t.DriverId)
                .Index(t => t.VehicleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AssignedRequests", "VehicleId", "dbo.Vehicles");
            DropForeignKey("dbo.AssignedRequests", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.AssignedRequests", "DriverId", "dbo.Drivers");
            DropIndex("dbo.AssignedRequests", new[] { "VehicleId" });
            DropIndex("dbo.AssignedRequests", new[] { "DriverId" });
            DropIndex("dbo.AssignedRequests", new[] { "RequestId" });
            DropTable("dbo.AssignedRequests");
        }
    }
}
