namespace VehicleRequisitionSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblRequest_AssignedRequisitionId_Removed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Requests", "AssignedRequisitionId", "dbo.AssignedRequisitions");
            DropIndex("dbo.Requests", new[] { "AssignedRequisitionId" });
            DropColumn("dbo.Requests", "AssignedRequisitionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Requests", "AssignedRequisitionId", c => c.Int());
            CreateIndex("dbo.Requests", "AssignedRequisitionId");
            AddForeignKey("dbo.Requests", "AssignedRequisitionId", "dbo.AssignedRequisitions", "Id");
        }
    }
}
