using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.Model.Extension
{
    public static class PayCodeDictionaryExtensions
    {
        public static bool IsOTE(this Dictionary<string, PayCode> paycodeDict, string paycode)
        {
            return paycodeDict.ContainsKey(paycode) ? paycodeDict[paycode].IsOTE : false;
        }
    }
}
