namespace VehicleRequisitionSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class string_No_to_VehicleTblAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "No", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "No");
        }
    }
}
