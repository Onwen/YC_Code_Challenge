using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.Model.Configuration
{
    public class SuperDataConfiguration(string filepath)
    {
        public string filepath { get; set; } = filepath;
    }
}
