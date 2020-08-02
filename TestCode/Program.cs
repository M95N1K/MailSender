using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code;

namespace TestCode
{
    class Program
    {
        static void Main(string[] args)
        {
            string tst = "HomeVideo";
            tst = PassCode.Encrypt(tst, "pass");
            Console.WriteLine(tst);
            tst = PassCode.Decrypt(tst, "pass");
            Console.WriteLine(tst);
            Console.ReadLine();
        }
    }
}
