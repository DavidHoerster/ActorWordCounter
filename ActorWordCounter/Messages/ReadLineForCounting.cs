using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAkka1.Messages
{
    public class ReadLineForCounting
    {
        public readonly String Line;

        public ReadLineForCounting(String line) { Line = line; }
    }
}
