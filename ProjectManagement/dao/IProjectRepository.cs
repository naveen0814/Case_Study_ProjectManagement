using ProjectManagement.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ProjectManagement.DAO
{
    public interface IProjectRepository
    {
        bool CreateEmployee(Employee emp);
        bool CreateProject(Project proj);
        bool CreateTask(ProjectTask task);
        bool AssignProjectToEmployee(int projectId, int employeeId);
        bool AssignTaskToEmployee(int taskId, int projectId, int employeeId);
        bool DeleteEmployee(int empId);
        bool DeleteProject(int projectId);
        List<ProjectTask> GetAllTasks(int empId, int projectId);
        List<Employee> GetAllEmployees();
        List<Project> GetAllProjects();
        List<ProjectTask> GetAllProjectTasks();
        bool DeleteTask(int taskId);
    }
}