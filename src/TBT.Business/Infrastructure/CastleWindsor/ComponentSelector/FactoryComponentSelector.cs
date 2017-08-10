using Castle.Facilities.TypedFactory;
using System.Reflection;

namespace TBT.Business.Infrastructure.CastleWindsor.ComponentSelector
{
    public class FactoryComponentSelector : DefaultTypedFactoryComponentSelector
    {
        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            return (string)arguments[0];
        }
    }
}
