using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCCodingChallenge.Model;

namespace YCCodingChallenge.Service.Interface
{
    public interface ISuperDisbursementService
    {
        public List<QuarterlyDetails> CalculateSuperDisbursements();
    }
}
