namespace DAL.Clients.Interfaces
{
    using Models.Domain.Models;
    using System.Threading.Tasks;

    public interface IDepartmentClient
    {
        Task<Department> GetDefaultDepartment();
    }
}
