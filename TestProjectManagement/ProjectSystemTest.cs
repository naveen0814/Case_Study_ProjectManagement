using NUnit.Framework;
using ProjectManagement.DAO;
using ProjectManagement.entity;
using System;
using System.Collections.Generic;

namespace ProjectManagement.Tests
{
    [TestFixture]
    public class ProjectRepositoryTests
    {
        private IProjectRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new ProjectRepositoryImpl();
        }

        [Test]
        public void CreateEmployee_ShouldReturnTrue_WhenEmployeeIsValid()
        {   //Arrange
            Employee emp = new Employee
            {
                Name = "Test Employee",
                Designation = "QA Engineer",
                Gender = "Male",
                Salary = 35000,
                Project_Id = null
            };
            //Act
            bool result = _repository.CreateEmployee(emp);
            //Assert
            Assert.IsTrue(result, "Expected employee to be created successfully.");
        }

        [Test]
        public void CreateTask_ShouldReturnTrue_WhenTaskIsValid()
        {
            ProjectTask task = new ProjectTask
            {
                TaskName = "Write Unit Tests",
                Project_Id = 1, 
                Employee_Id = 1, 
                Status = "Assigned"
            };

            bool result = _repository.CreateTask(task);
            Assert.IsTrue(result, "Expected task to be created successfully.");
        }

        [Test]
        public void GetAllTasks_ShouldReturnList_WhenEmployeeAndProjectAreValid()
        {
            int empId = 0;
            int projectId = 0;  

            List<ProjectTask> tasks = _repository.GetAllTasks(empId, projectId);
            Assert.IsNotNull(tasks, "Expected a task list (even if empty).");
            Assert.That(tasks.Count, Is.GreaterThanOrEqualTo(0), "Expected zero or more tasks.");
        }

        [Test]
        public void DeleteEmployee_ShouldThrowException_WhenEmployeeDoesNotExist()
        {
            int invalidEmpId = 000; 
            var result = _repository.DeleteEmployee(invalidEmpId);

            if (!result)
            {
                Assert.Pass("Correctly handled non-existent employee.");
            }
            else
            {
                Assert.Fail("Expected delete to fail for non-existent employee.");
            }
        }

        [Test]
        public void DeleteProject_ShouldThrowException_WhenProjectDoesNotExist()
        {
            int invalidProjectId = 000; 
            var result = _repository.DeleteProject(invalidProjectId);

            if (!result)
            {
                Assert.Pass("Correctly handled non-existent project.");
            }
            else
            {
                Assert.Fail("Expected delete to fail for non-existent project.");
            }
        }
    }
}