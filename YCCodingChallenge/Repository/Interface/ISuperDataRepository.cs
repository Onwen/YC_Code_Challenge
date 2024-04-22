using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YCCodingChallenge.DTO;
using YCCodingChallenge.Model;

namespace YCCodingChallenge.Repository.Interface
{
    public interface ISuperDataRepository
    {
        public SuperDataDTO GetSuperData();
    }
}
