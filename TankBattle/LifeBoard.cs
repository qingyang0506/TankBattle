using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    class LifeBoard
    {
        public static int Life { get; set; }
        public static Graphics LifeG;
        public static void Update()
        {
            string str = "Life: " + Life;
            LifeG.DrawString(str, new Font("Arial", 16), new SolidBrush(Color.Black), new Point(20, 6));
        }
    }
}
