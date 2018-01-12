using StrategyCorps.CodeSample.Interfaces.Core;
using StructureMap;

namespace StrategyCorps.CodeSample.Repositories
{
    public class StartUp:IStartUp
    {
        public void Execute(IContainer container)
        {
            container.Configure(c =>
            {
                //add registry here
                //c.AddRegistry<DefaultRepositoriesegistry>();
            });

            //add mapping profile here
            //Mapper.AddProfile<MemberMappingProfile>();
        }
    }
}
