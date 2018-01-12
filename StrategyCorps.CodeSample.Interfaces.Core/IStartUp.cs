using StructureMap;

namespace StrategyCorps.CodeSample.Interfaces.Core
{
    public interface IStartUp
    {
        void Execute(IContainer container);
    }
}
