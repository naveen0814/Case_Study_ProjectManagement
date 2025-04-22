CREATE DATABASE PROJECTMANAGEMENT
USE PROJECTMANAGEMENT

CREATE TABLE Project (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProjectName VARCHAR(100),
    Description TEXT,
    StartDate DATE,
    Status VARCHAR(50) -- started, dev, build, test, deployed
);

CREATE TABLE Employee (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100),
    Designation VARCHAR(100),
    Gender VARCHAR(10),
    Salary DECIMAL(10,2),
    Project_Id INT,
    FOREIGN KEY (Project_Id) REFERENCES Project(Id)
);

CREATE TABLE Task (
    Task_Id INT PRIMARY KEY IDENTITY(1,1),
    Task_Name VARCHAR(100),
    Project_Id INT,
    Employee_Id INT,
    Status VARCHAR(50), -- Assigned, started, completed
    FOREIGN KEY (Project_Id) REFERENCES Project(Id),
    FOREIGN KEY (Employee_Id) REFERENCES Employee(Id)
);


INSERT INTO Project (ProjectName, Description, StartDate, Status) VALUES
('Inventory System', 'Manage warehouse stocks and product movements', '2024-01-10', 'started'),
('E-Commerce Platform', 'Online store for electronics', '2024-02-01', 'dev'),
('HR Portal', 'Employee self-service portal', '2024-03-15', 'test'),
('Banking App', 'Mobile app for banking operations', '2024-01-25', 'build'),
('School Management', 'System to manage school records', '2024-02-20', 'deployed');
GO


INSERT INTO Employee (Name, Designation, Gender, Salary, Project_Id) VALUES
('Alice Johnson', 'Developer', 'Female', 75000.00, 1),
('Bob Smith', 'Tester', 'Male', 65000.00, 2),
('Charlie Lee', 'Team Lead', 'Male', 85000.00, 3),
('Diana Patel', 'Developer', 'Female', 72000.00, 4),
('Ethan Brown', 'Analyst', 'Male', 70000.00, 5)


INSERT INTO Task (Task_Name, Project_Id, Employee_Id, Status) VALUES
('Design DB Schema', 1, 1, 'Completed'),
('Write Test Cases', 2, 2, 'Started'),
('Conduct Review Meeting', 3, 3, 'Assigned'),
('Implement Login Module', 4, 4, 'Started'),
('Prepare Report', 5, 5, 'Completed')

ALTER TABLE Employee
ADD CONSTRAINT CK_Employee_Name_NotNumeric1 
CHECK (Name NOT LIKE '%[^A-Za-z ]%');

ALTER TABLE Employee
ADD CONSTRAINT CK_Employee_Designation_NotNumeric2 
CHECK (Designation NOT LIKE '%[^A-Za-z ]%');

ALTER TABLE Employee
ADD CONSTRAINT CK_Employee_Gender_Values3 
CHECK (Gender IN ('Male', 'Female', 'Other'));