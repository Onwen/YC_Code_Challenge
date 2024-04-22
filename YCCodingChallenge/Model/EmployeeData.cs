using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.Model
{
    public class EmployeeData
    {
        public List<Employee> Employees { get; set; } = [];
        public Dictionary<string, PayCode> Paycodes { get; set; } = [];
    }
}
