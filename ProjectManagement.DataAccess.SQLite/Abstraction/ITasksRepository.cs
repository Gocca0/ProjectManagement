using ProjectManagement.DataAccess.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.DataAccess.SQLite.Abstraction
{
    public interface ITasksRepository
    {
        public Task<List<TaskEntity>> Get();

        public Task<TaskEntity> Get(int id);

        public Task Add(TaskEntity task);

        public Task Update(TaskEntity task);

        public Task Delete(int id);
    }
}
