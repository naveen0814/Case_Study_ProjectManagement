using System;
using System.Collections.Generic;
using ProjectManagement.DAO;
using ProjectManagement.entity;
using ProjectManagement.exception;

namespace ProjectManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            IProjectRepository repository = new ProjectRepositoryImpl();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n       Project Management Menu      ");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Add Project");
                Console.WriteLine("3. Add Task");
                Console.WriteLine("4. Assign Project to Employee");
                Console.WriteLine("5. Assign Task to Employee");
                Console.WriteLine("6. Delete Employee");
                Console.WriteLine("7. Delete Project");
                Console.WriteLine("8. View Tasks for Employee in a Project");
                Console.WriteLine("9. View All Employees");
                Console.WriteLine("10. View All Projects");
                Console.WriteLine("11. View All Tasks");
                Console.WriteLine("12. View All Tables");
                Console.WriteLine("13. Delete Task");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");

                try
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            try
                            {
                                Employee emp = new Employee();
                                Console.Write("Enter Name: "); emp.Name = Console.ReadLine();
                                Console.Write("Enter Designation: "); emp.Designation = Console.ReadLine();
                                Console.Write("Enter Gender: "); emp.Gender = Console.ReadLine();
                                Console.Write("Enter Salary: "); emp.Salary = decimal.Parse(Console.ReadLine());
                                Console.Write("Enter Project Id (or leave blank): ");
                                string pid = Console.ReadLine();
                                emp.Project_Id = string.IsNullOrEmpty(pid) ? null : int.Parse(pid);
                                Console.WriteLine(repository.CreateEmployee(emp) ? "Employee added." : "Failed to add employee.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error adding employee: " + ex.Message);
                            }
                            break;

                        case "2":
                            try
                            {
                                Project proj = new Project();
                                Console.Write("Enter Project Name: "); proj.ProjectName = Console.ReadLine();
                                Console.Write("Enter Description: "); proj.Description = Console.ReadLine();
                                Console.Write("Enter Start Date (yyyy-mm-dd): "); proj.StartDate = DateTime.Parse(Console.ReadLine());
                                Console.Write("Enter Status: "); proj.Status = Console.ReadLine();
                                Console.WriteLine(repository.CreateProject(proj) ? "Project added." : "Failed to add project.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error adding project: " + ex.Message);
                            }
                            break;

                        case "3":
                            try
                            {
                                ProjectTask task = new ProjectTask();
                                Console.Write("Enter Task Name: "); task.TaskName = Console.ReadLine();
                                Console.Write("Enter Project Id: "); task.Project_Id = int.Parse(Console.ReadLine());
                                Console.Write("Enter Employee Id: "); task.Employee_Id = int.Parse(Console.ReadLine());
                                Console.Write("Enter Status: "); task.Status = Console.ReadLine();
                                Console.WriteLine(repository.CreateTask(task) ? "Task added." : "Failed to add task.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error adding task: " + ex.Message);
                            }
                            break;

                        case "4":
                            try
                            {
                                Console.Write("Enter Project Id: "); int pId = int.Parse(Console.ReadLine());
                                Console.Write("Enter Employee Id: "); int eId = int.Parse(Console.ReadLine());
                                Console.WriteLine(repository.AssignProjectToEmployee(pId, eId) ? "Project assigned to employee." : "Failed.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error assigning project: " + ex.Message);
                            }
                            break;

                        case "5":
                            try
                            {
                                Console.Write("Enter Task Id: "); int tId = int.Parse(Console.ReadLine());
                                Console.Write("Enter Project Id: "); int prjId = int.Parse(Console.ReadLine());
                                Console.Write("Enter Employee Id: "); int empId = int.Parse(Console.ReadLine());
                                Console.WriteLine(repository.AssignTaskToEmployee(tId, prjId, empId) ? "Task assigned." : "Failed.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error assigning task: " + ex.Message);
                            }
                            break;

                        case "6":
                            try
                            {
                                Console.Write("Enter Employee Id to delete: "); int delEmpId = int.Parse(Console.ReadLine());
                                Console.WriteLine(repository.DeleteEmployee(delEmpId) ? "Employee deleted." : "Failed.");
                            }
                            catch (EmployeeNotFoundException enfe)
                            {
                                Console.WriteLine("Employee not found: " + enfe.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error deleting employee: " + ex.Message);
                            }
                            break;

                        case "7":
                            try
                            {
                                Console.Write("Enter Project Id to delete: "); int delProjId = int.Parse(Console.ReadLine());
                                Console.WriteLine(repository.DeleteProject(delProjId) ? "Project deleted." : "Failed.");
                            }
                            catch (ProjectNotFoundException pnfe)
                            {
                                Console.WriteLine("Project not found: " + pnfe.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error deleting project: " + ex.Message);
                            }
                            break;

                        case "8":
                            try
                            {
                                Console.Write("Enter Employee Id: "); int viewEmpId = int.Parse(Console.ReadLine());
                                Console.Write("Enter Project Id: "); int viewProjId = int.Parse(Console.ReadLine());
                                List<ProjectTask> tasks = repository.GetAllTasks(viewEmpId, viewProjId);
                                Console.WriteLine($"\nTasks for Employee ID {viewEmpId} in Project ID {viewProjId}:");
                                foreach (var t in tasks)
                                {
                                    Console.WriteLine($"- {t.TaskName} ({t.Status})");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error retrieving tasks: " + ex.Message);
                            }
                            break;

                        case "9":
                            try
                            {
                                Console.WriteLine("\nAll Employees:");
                                var employees = repository.GetAllEmployees();
                                foreach (var e in employees)
                                {
                                    Console.WriteLine($"ID: {e.Id}, Name: {e.Name}, Designation: {e.Designation}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error retrieving employees: " + ex.Message);
                            }
                            break;

                        case "10":
                            try
                            {
                                Console.WriteLine("\nAll Projects:");
                                var projects = repository.GetAllProjects();
                                foreach (var p in projects)
                                {
                                    Console.WriteLine($"ID: {p.Id}, Name: {p.ProjectName}, Status: {p.Status}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error retrieving projects: " + ex.Message);
                            }
                            break;

                        case "11":
                            try
                            {
                                Console.WriteLine("\nAll Tasks:");
                                var allTasks = repository.GetAllProjectTasks();
                                foreach (var t in allTasks)
                                {
                                    Console.WriteLine($"ID: {t.Task_Id}, Name: {t.TaskName}, Assigned To: {t.Employee_Id}, Project: {t.Project_Id}, Status: {t.Status}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error retrieving tasks: " + ex.Message);
                            }
                            break;

                        case "12":
                            try
                            {
                                Console.WriteLine("\nAll Employees:");
                                foreach (var e in repository.GetAllEmployees())
                                {
                                    Console.WriteLine($"ID: {e.Id}, Name: {e.Name}, Designation: {e.Designation}");
                                }

                                Console.WriteLine("\nAll Projects:");
                                foreach (var p in repository.GetAllProjects())
                                {
                                    Console.WriteLine($"ID: {p.Id}, Name: {p.ProjectName}, Status: {p.Status}");
                                }

                                Console.WriteLine("\nAll Tasks:");
                                foreach (var t in repository.GetAllProjectTasks())
                                {
                                    Console.WriteLine($"ID: {t.Task_Id}, Name: {t.TaskName}, Assigned To: {t.Employee_Id}, Project: {t.Project_Id}, Status: {t.Status}");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error retrieving tables: " + ex.Message);
                            }
                            break;

                        case "13":
                            try
                            {
                                Console.Write("Enter Task Id to delete: "); int delTaskId = int.Parse(Console.ReadLine());
                                Console.WriteLine(repository.DeleteTask(delTaskId) ? "Task deleted." : "Failed.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error deleting task: " + ex.Message);
                            }
                            break;

                        case "0":
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected error: " + ex.Message);
                }
            }

            Console.WriteLine("\nExiting Project Management System. Goodbye!");
        }
    }
}
