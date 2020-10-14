namespace Tests.Integration
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Tests.Integration.Helpers;

    [TestClass]
    public class Setup
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            Configurations.Init();
            DatabaseConnection.Init(Configurations.Current.MongoDBConnection);
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            DbCleaner.Clean();
        }
    }
}
