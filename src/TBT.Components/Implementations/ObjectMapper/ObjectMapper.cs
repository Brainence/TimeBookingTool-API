using TBT.Components.Interfaces.ObjectMapper;

namespace TBT.Components.Implementations.ObjectMapper
{
    public class ObjectMapper : IObjectMapper
    {
        public TFrom Map<TTo, TFrom>(TTo value)
        {
            var res = AutoMapper.Mapper.Map<TTo, TFrom>(value);
            return res;
        }
    }
}
