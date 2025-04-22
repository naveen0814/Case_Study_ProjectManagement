using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.entity
{
    public class Employee
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Gender { get; set; }
        public decimal Salary { get; set; }
        public int? Project_Id { get; set; }
    }

}
