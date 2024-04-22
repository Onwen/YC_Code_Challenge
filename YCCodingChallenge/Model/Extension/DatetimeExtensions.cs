using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCodingChallenge.Model.Extension
{
    public static class DatetimeExtensions
    {
        public static int Quarter(this DateTime dateTime) {  return (dateTime.Month - 1) / 3 + 1; }
    }
}
