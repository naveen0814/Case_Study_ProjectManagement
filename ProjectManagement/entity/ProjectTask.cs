using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.entity
{
    public class ProjectTask
    {
        public int Task_Id { get; set; }
        public string TaskName { get; set; }
        public int Project_Id { get; set; }
        public int Employee_Id { get; set; }
        public string Status { get; set; }
    }
}
