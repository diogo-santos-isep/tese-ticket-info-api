namespace DAL.Clients.Interfaces
{
    using Models.Domain.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserClient
    {
        Task<IEnumerable<User>> GetCollaboratorsFromDepartment(Department defaultDepartment);
    }
}
