using ProjectManagement.DataAccess.SQLite.Models;

namespace ProjectManagement.API.RequestModels.Employee
{
    public class EmployeeAddRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } = "@mail.ru";
    }
}
