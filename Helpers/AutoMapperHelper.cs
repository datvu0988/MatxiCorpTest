using AutoMapper;
using matxicorp.Data.Entities;
using matxicorp.Models;

namespace matxicorp.Helpers
{
    public class AutoMapperHelper : AutoMapperHelperBase
    {
        private static AutoMapperHelper _instance = null;
        private static readonly object _padlock = new object();

        public static AutoMapperHelper Instance
        {
            get
            {
                lock (_padlock)
                {
                    if (_instance == null)
                        _instance = new AutoMapperHelper();
                }
                return _instance;
            }
        }

        protected override MapperConfiguration RegisterMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddEmployeeViewModel, Employee>().ReverseMap();
                cfg.CreateMap<EditEmployeeViewModel, Employee>().ReverseMap();
                cfg.CreateMap<BasicEmployeeInfo, Employee>().ReverseMap();
                cfg.CreateMap<EmployeeDetailViewModel, Employee>().ReverseMap();
                cfg.CreateMap<DeleteEmployeeViewModel, Employee>().ReverseMap();
            });

            return config;
        }
    }
}
