namespace ITSingular.TesteNET.Data.Context
{
    using ITSingular.TesteNET.Data.EntityConfig;
    using ITSingular.TesteNET.DataTransfer;
    using ITSingular.TesteNET.DataTransfer.Entityes;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class TesteNETContext : DbContext
    {
        public TesteNETContext() : base("TesteNETContext")
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 42000;
            this.Configuration.UseDatabaseNullSemantics = true;
            //this.Database.Log = msg => Trace.WriteLine(msg);
            //this.Configuration.LazyLoadingEnabled = true;
            // the terrible hack ( devido ao erro que ocorre no windows service X EF).
            //var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        #region Configuracacao
        public IDbSet<MachineInformation> MachineInformation { get; set; }
        public IDbSet<MachineInformationApplication> MachineInformationApplication { get; set; }
        #endregion Configuracacao

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Congifure Geral

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<decimal>()
                .Configure(p => p.HasPrecision(18, 6));

            #endregion Configure Geral

            #region Add Configurations

            modelBuilder.Configurations.Add(new MachineInformationConfiguration());
            modelBuilder.Configurations.Add(new MachineInformationApplicationConfiguration());
            
            #endregion Add Configurations

            #region Model Creating

            base.OnModelCreating(modelBuilder);

            #endregion Model Creating
        }
    }
}