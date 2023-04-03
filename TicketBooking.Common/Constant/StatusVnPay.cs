using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBooking.Common.Constant
{
    public class StatusVnPay
    {
        public const string Success = "00";
        public const string Fail = "01";
        public const string Error = "02";
        public static string GetName(string code)
        {
            foreach (var field in typeof(StatusVnPay).GetFields())
            {
                if ((string)field.GetValue(null) == code)
                    return field.Name.ToString();
            }
            return "";
        }
    }
}
