namespace matxicorp.Data.Contracts
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository EmployeeRepository { get; }
        void Save();
    }
}
