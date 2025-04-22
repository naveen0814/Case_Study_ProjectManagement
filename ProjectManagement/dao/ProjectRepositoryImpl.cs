using ProjectManagement.entity;
using ProjectManagement.util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ProjectManagement.DAO
{
    public class ProjectRepositoryImpl : IProjectRepository
    {
        public bool CreateEmployee(Employee emp)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Employee (Name, Designation, Gender, Salary, Project_Id) VALUES (@name, @designation, @gender, @salary, @projectId)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", emp.Name);
                cmd.Parameters.AddWithValue("@designation", emp.Designation);
                cmd.Parameters.AddWithValue("@gender", emp.Gender);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@projectId", (object)emp.Project_Id ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CreateProject(Project proj)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Project (ProjectName, Description, StartDate, Status) VALUES (@name, @desc, @date, @status)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", proj.ProjectName);
                cmd.Parameters.AddWithValue("@desc", proj.Description);
                cmd.Parameters.AddWithValue("@date", proj.StartDate);
                cmd.Parameters.AddWithValue("@status", proj.Status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CreateTask(ProjectTask task)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Task (Task_Name, Project_Id, Employee_Id, Status) VALUES (@name, @pid, @eid, @status)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", task.TaskName);
                cmd.Parameters.AddWithValue("@pid", task.Project_Id);
                cmd.Parameters.AddWithValue("@eid", task.Employee_Id);
                cmd.Parameters.AddWithValue("@status", task.Status);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool AssignProjectToEmployee(int projectId, int employeeId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();

                try
                {
                   
                    string updateEmployee = "UPDATE Employee SET Project_Id = @pid WHERE Id = @eid";
                    SqlCommand cmdEmp = new SqlCommand(updateEmployee, conn, tx);
                    cmdEmp.Parameters.AddWithValue("@pid", projectId);
                    cmdEmp.Parameters.AddWithValue("@eid", employeeId);
                    cmdEmp.ExecuteNonQuery();

                    
                    string updateTasks = "UPDATE Task SET Project_Id = @pid WHERE Employee_Id = @eid";
                    SqlCommand cmdTask = new SqlCommand(updateTasks, conn, tx);
                    cmdTask.Parameters.AddWithValue("@pid", projectId);
                    cmdTask.Parameters.AddWithValue("@eid", employeeId);
                    cmdTask.ExecuteNonQuery();

                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    return false;
                }
            }
        }

        public bool AssignTaskToEmployee(int taskId, int projectId, int employeeId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();

                try
                {
     
                    string updateTask = "UPDATE Task SET Project_Id = @pid, Employee_Id = @eid WHERE Task_Id = @tid";
                    SqlCommand cmdTask = new SqlCommand(updateTask, conn, tx);
                    cmdTask.Parameters.AddWithValue("@pid", projectId);
                    cmdTask.Parameters.AddWithValue("@eid", employeeId);
                    cmdTask.Parameters.AddWithValue("@tid", taskId);
                    cmdTask.ExecuteNonQuery();

                
                    string updateEmployee = "UPDATE Employee SET Project_Id = @pid WHERE Id = @eid";
                    SqlCommand cmdEmp = new SqlCommand(updateEmployee, conn, tx);
                    cmdEmp.Parameters.AddWithValue("@pid", projectId);
                    cmdEmp.Parameters.AddWithValue("@eid", employeeId);
                    cmdEmp.ExecuteNonQuery();

                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    return false;
                }
            }
        }

        public bool DeleteEmployee(int empId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Employee WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", empId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteProject(int projectId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Project WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", projectId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public List<Employee> GetAllEmployees()
{
    List<Employee> employees = new List<Employee>();
    using (SqlConnection conn = DBConnUtil.GetConnection())
    {
        string query = "SELECT * FROM Employee";
        SqlCommand cmd = new SqlCommand(query, conn);
        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            employees.Add(new Employee
            {
                Id = (int)reader["Id"],
                Name = reader["Name"].ToString(),
                Designation = reader["Designation"].ToString(),
                Gender = reader["Gender"].ToString(),
                Salary = (decimal)reader["Salary"],
                Project_Id = reader["Project_Id"] != DBNull.Value ? (int?)reader["Project_Id"] : null
            });
        }
    }
    return employees;
}
        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Project";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    projects.Add(new Project
                    {
                        Id = (int)reader["Id"],
                        ProjectName = reader["ProjectName"].ToString(),
                        Description = reader["Description"].ToString(),
                        StartDate = (DateTime)reader["StartDate"],
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return projects;
        }
        public List<ProjectTask> GetAllProjectTasks()
        {
            List<ProjectTask> tasks = new List<ProjectTask>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Task";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tasks.Add(new ProjectTask
                    {
                        Task_Id = (int)reader["Task_Id"],
                        TaskName = reader["Task_Name"].ToString(),
                        Project_Id = (int)reader["Project_Id"],
                        Employee_Id = (int)reader["Employee_Id"],
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return tasks;
        }
        public bool DeleteTask(int taskId)
        {
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Task WHERE Task_Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", taskId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }



        public List<ProjectTask> GetAllTasks(int empId, int projectId)
        {
            List<ProjectTask> tasks = new List<ProjectTask>();
            using (SqlConnection conn = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Task WHERE Employee_Id = @eid AND Project_Id = @pid";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@eid", empId);
                cmd.Parameters.AddWithValue("@pid", projectId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tasks.Add(new ProjectTask
                    {
                        Task_Id = (int)reader["Task_Id"],
                        TaskName = reader["Task_Name"].ToString(),
                        Project_Id = (int)reader["Project_Id"],
                        Employee_Id = (int)reader["Employee_Id"],
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return tasks;
        }
    }
}