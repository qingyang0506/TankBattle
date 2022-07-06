using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    enum GameState
    {
        Running,
        GameOver
    }
    class GameFramework
    {

        public static Graphics g;
        private static GameState gamestate = GameState.Running;
         public static void Start()
        {
            SoundManager.initSound();
            SoundManager.PlayStart();
            GameObjectManager.start();
            GameObjectManager.CreateMap();
            GameObjectManager.createMyTank();
        }

        public static void Update()
        {
            //FPS
            if(gamestate == GameState.Running)
            {
                GameObjectManager.Update();
            }else if(gamestate == GameState.GameOver)
            {
                GameOverUpdate();
            }
            
        }

         public static void ChangeToGameOver()
        {
            gamestate = GameState.GameOver;
        }

        private static void GameOverUpdate()
        {
            int x = 450/2 - Properties.Resources.GameOver.Width/2;
            int y = 450 / 2 -Properties.Resources.GameOver.Height / 2;
            g.DrawImage(Properties.Resources.GameOver, x, y);
            string str = "Score: " + ScoreBoard.Score;
            g.DrawString(str, new Font("Arial", 20), new SolidBrush(Color.White), new Point(170,340));
        }
         
    }
}
