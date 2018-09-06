namespace VehicleRequisitionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequisitionRequest_IsCanceled_Introduce : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RequisitionRequests", "IsCanceled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RequisitionRequests", "IsCanceled");
        }
    }
}
