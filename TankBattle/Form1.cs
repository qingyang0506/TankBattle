using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public partial class Form1 : Form
    {
        private Thread t;
        public static Graphics windowG;
        private static Bitmap bmp;
        private static Bitmap bmp1;
        private static Bitmap bmp2;
        public Form1()
        {
            InitializeComponent();

            //set the startPosition for windows
            this.StartPosition = FormStartPosition.CenterScreen;

            windowG = this.CreateGraphics();
            

            bmp = new Bitmap(450, 450);
            Graphics bmpG = Graphics.FromImage(bmp);
            GameFramework.g = bmpG;

            bmp1 = new Bitmap(130, 35);
            Graphics bmpG1 = Graphics.FromImage(bmp1);
            ScoreBoard.scoreG= bmpG1;

            bmp2 = new Bitmap(130, 35);
            Graphics bmpG2 = Graphics.FromImage(bmp2);
            LifeBoard.LifeG = bmpG2;

            //block
            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();
        }

        private static void GameMainThread()
        {
            //GameFramework
            GameFramework.Start();


            int sleepTime = 1000 / 60;
            while (true)
            {
                GameFramework.g.Clear(Color.Black);
                GameFramework.Update();
                ScoreBoard.scoreG.Clear(Color.White);
                ScoreBoard.Update();
                LifeBoard.LifeG.Clear(Color.White);
                LifeBoard.Update();
                Form1.windowG.DrawImage(bmp,0,0);
                Form1.windowG.DrawImage(bmp1, 300, 460);
                Form1.windowG.DrawImage(bmp2, 20, 460);
                Thread.Sleep(sleepTime); 
            }
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.keyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.keyUp(e);
        }
    }
}
