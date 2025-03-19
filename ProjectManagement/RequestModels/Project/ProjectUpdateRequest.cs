using ProjectManagement.DataAccess.SQLite.Models;

namespace ProjectManagement.API.RequestModels.Project
{
    public class ProjectUpdateRequest : ProjectAddRequest
    {
        public int Id { get; set; }
    }
}
