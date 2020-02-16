using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkchainA_DiscordBot
{
    class Virion
    {
        static int memoryV1 = 0;

        public double GetRandomV1(double Min = 0, double Max = 1)
        {
            if (memoryV1 > 268)
            {
                memoryV1 = 12;
            }
            memoryV1 += 14;

            {
                double Dno;
                string now = DateTime.Now.ToString("fffffff");
                Double.TryParse(now, out Dno);
                Dno = Dno / 10000000;
                Dno = memoryV1 * Dno;

                Dno = 1 - (Dno - (int)Dno);

                Dno = Min + ((Max - Min) * Dno);

                return Dno;
            }
        }
    }
}
