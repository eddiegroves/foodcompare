using StructureMap;
namespace FoodCompare.Web.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });

                x.For<System.Data.IDbConnection>().HttpContextScoped().Use(() =>
                     FoodCompare.Web.Data.Database.Factory.OpenDbConnection()
                );
            });

            return ObjectFactory.Container;
        }
    }
}
