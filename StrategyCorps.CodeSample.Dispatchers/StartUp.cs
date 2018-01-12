using StrategyCorps.CodeSample.Dispatchers.Registries;
using StrategyCorps.CodeSample.Interfaces.Core;
using StructureMap;

namespace StrategyCorps.CodeSample.Dispatchers
{
    public class StartUp:IStartUp
    {
        //private MapperConfiguration _mapperConfiguration;
        //public StartUp(MapperConfiguration mapperConfiguration)
        //{
        //    _mapperConfiguration = mapperConfiguration;
        //}
        public void Execute(IContainer container)
        {
            container.Configure(c =>
            {
                c.AddRegistry<DefaultDispatchersRegistry>();
            });

            //add mapping profile here
            //_mapperConfiguration.AddProfiles<MemberMappingProfile>();
        }
    }
}
