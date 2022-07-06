using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    class ScoreBoard
    {
        public static int Score { get; set; }
        public static Graphics scoreG;

        public static void Update()
        {
            Graphics g = scoreG;
            string str = "Score: " + Score;
            g.DrawString(str, new Font("Arial", 16), new SolidBrush(Color.Black), new Point(20, 6));
        }
    }
}
