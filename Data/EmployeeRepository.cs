using matxicorp.Data.Contracts;
using matxicorp.Data.Entities;
using matxicorp.Data.Repository;

namespace matxicorp.Data
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
