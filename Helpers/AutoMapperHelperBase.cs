using AutoMapper;

namespace matxicorp.Helpers
{
    public abstract class AutoMapperHelperBase
    {
        private IMapper _mapper;

        public AutoMapperHelperBase()
        {
            MapperConfiguration config = RegisterMapper();
            _mapper = config.CreateMapper();
        }

        public Destination Map<Source, Destination>(Source source)
        {
            return _mapper.Map<Source, Destination>(source);
        }

        protected abstract MapperConfiguration RegisterMapper();
    }
}
