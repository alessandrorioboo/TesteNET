namespace ITSingular.TesteNET.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MachineInformation",
                c => new
                {
                    IdMachineInformation = c.Guid(nullable: false),
                    MachineName = c.String(nullable: false, maxLength: 256, unicode: false),
                    DateTimeUTC = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.IdMachineInformation);

            CreateTable(
                "dbo.MachineInformationApplication",
                c => new
                {
                    IdMachineInformationApplication = c.Guid(nullable: false),
                    Applicatiion = c.String(nullable: false, maxLength: 256, unicode: false),
                    MachineInformationId = c.Guid(nullable: false),
                })
                .PrimaryKey(t => t.IdMachineInformationApplication)
                .ForeignKey("dbo.MachineInformation", t => t.MachineInformationId)
                .Index(t => t.MachineInformationId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.MachineInformationApplication", "MachineInformationId", "dbo.MachineInformation");
            DropIndex("dbo.MachineInformationApplication", new[] { "MachineInformationId" });
            DropTable("dbo.MachineInformationApplication");
            DropTable("dbo.MachineInformation");
        }
    }
}
