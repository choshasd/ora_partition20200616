using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oraConn01
{
      class RandData
      {
            static string[] first = { "회", "은", "경", "진", "호" };
            static string[] mid = { "성", "희", "채", "영", "명" };
            static string[] last = { "조", "김", "권", "박", "한" };
            static int[] age = { 25, 29, 27, 37, 36 };
            static string[] addr = { "대구", "울산", "평양", "구미", "영덕" };

            static Random r = new Random();

            public static string getName()
            {
                  string fullName = last[r.Next(0,5)] + mid[r.Next(0,5)] + first[r.Next(0,5)];
                  return fullName;
            }
            public static int getAge()
            {
                  return age[r.Next(0, 5)];
            }
            public static string getAddr()
            {
                  return addr[r.Next(0, 5)];
            }
      }
}
