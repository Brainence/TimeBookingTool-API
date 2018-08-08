using TBT.Components.Interfaces.ObjectMapper;

namespace TBT.Components.Implementations.ObjectMapper
{
    public class ObjectMapper : IObjectMapper
    {
        public TFrom Map<TTo, TFrom>(TTo value)
        {
            return AutoMapper.Mapper.Map<TTo, TFrom>(value);
            
        }
    }
}
