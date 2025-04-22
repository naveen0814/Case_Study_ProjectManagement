using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.exception
{
    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException(string message) : base(message) { }
    }
}
