namespace Employees.Interfaces
{
    public interface IServiceManager
    {
        public ICompanyService Company { get; }
        public IEmployeeService Employee { get; }
    }
}
