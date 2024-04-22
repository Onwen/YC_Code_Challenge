using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCCodingChallenge.DTO;
using YCCodingChallenge.Model;
using YCCodingChallenge.Repository.Interface;

namespace YCCodingChallenge.Adapter.Interface
{
    public interface ISuperDataRepositoryAdapter
    {
        public EmployeeData GetEmployeeData();
    }
}
