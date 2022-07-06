using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankBattle.Properties;

namespace TankBattle
{
    class GameObjectManager
    {
        private static List<NotMoveThing> wallList = new List<NotMoveThing>();
        private static List<NotMoveThing> steelList = new List<NotMoveThing>();
        private static NotMoveThing Boss;
        private static MyTank myTank;
        private static int enemyBornSpeed = 60;
        private static int enemyBornCount = 60;
        private static Point[] points = new Point[3];
        private static List<EnemyTank> tankList = new List<EnemyTank>();
        private static List<Bullet> bulletList = new List<Bullet>();
        private static Object bulletLock = new Object();
        private static List<Explosion> expList = new List<Explosion>();
        
        public static void start()
        {
            points[0].X = 0; points[0].Y = 0;
            points[1].X = 7 * 30; points[1].Y = 0;
            points[2].X = 14 * 30; points[2].Y = 0;
            ScoreBoard.Score = 0;
            LifeBoard.Life = 3;
        }
        public static void Update()
        {
            if(LifeBoard.Life <= 0)
            {
                GameFramework.ChangeToGameOver();
            }
            foreach (NotMoveThing nm in wallList)
            {
                nm.Update();
            }
            foreach (NotMoveThing nm in steelList)
            {
                nm.Update();
            }

            foreach(EnemyTank et in tankList)
            {
                et.Update();
            }

            CheckAndDestroyBullet();
            try
            {
                foreach (Bullet b in bulletList)
                {
                    b.Update();
                }
            }catch(Exception e)
            {

            }
            checkAndDestroyExplosion();
            foreach(Explosion exp in expList)
            {
                exp.Update();
            }
            
            Boss.Update();
            myTank.Update();
            EnemyBorn();
        }

        private static void CheckAndDestroyBullet()
        {
            List<Bullet> destroyBullet = new List<Bullet>();
            foreach(Bullet b in bulletList)
            {
                if (b.isDestroy)
                {
                    destroyBullet.Add(b);
                }
            }

            foreach(Bullet b in destroyBullet)
            {
                bulletList.Remove(b);
            }
            
        }

        private static void checkAndDestroyExplosion()
        {
            List<Explosion> needToDestroy = new List<Explosion>();
            foreach (Explosion exp in expList)
            {
                if (exp.isNeedDestroy)
                {
                    needToDestroy.Add(exp);
                }
            }

            foreach (Explosion exp in needToDestroy)
            {
                expList.Remove(exp);
            }
        }

        public static void CreateExplosion(int x,int y)
        {
            Explosion exp = new Explosion(x, y);
            expList.Add(exp);
            
        }
        public static void CreateBullet(int x,int y,Tag tag,Direction dir)
        {
            Bullet bullet = new Bullet(x, y, 8, dir, tag);

            bulletList.Add(bullet);

            
        }

        public static void DestroyWall(NotMoveThing wall)
        {
            wallList.Remove(wall);
        }

        public static void Destroytank(EnemyTank tank)
        {
            tankList.Remove(tank);
        }

        private static void EnemyBorn()
        {
            enemyBornCount++;
            if (enemyBornCount < enemyBornSpeed) return;

            SoundManager.PlayAdd();
            //0-2
            Random rand = new Random();
            int index = rand.Next(0, 3);
            Point position = points[index];
            int enemyType = rand.Next(1, 5);
            switch (enemyType)
            {
                case 1:
                    createEnemyTank1(position.X, position.Y);
                    break;
                case 2:
                    createEnemyTank2(position.X, position.Y);
                    break;
                case 3:
                    createEnemyTank3(position.X, position.Y);
                    break;
                case 4:
                    createEnemyTank4(position.X, position.Y);
                    break;

            }

            enemyBornCount = 0;
        }

        private static void createEnemyTank1(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GrayDown, Resources.GrayUp, Resources.GrayRight, Resources.GrayLeft);
            tankList.Add(tank);
        }
        private static void createEnemyTank2(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenDown, Resources.GreenUp, Resources.GreenRight, Resources.GreenLeft);
            tankList.Add(tank);
        }

        private static void createEnemyTank3(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4, Resources.QuickDown, Resources.QuickUp, Resources.QuickRight, Resources.QuickLeft);
            tankList.Add(tank);
        }

        private static void createEnemyTank4(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.SlowDown, Resources.SlowUp, Resources.SlowRight, Resources.SlowLeft);
            tankList.Add(tank);
        }

        public static NotMoveThing isCollideWall(Rectangle r)
        {
            foreach(NotMoveThing wall in wallList)
            {
                if (wall.getRectangle().IntersectsWith(r))
                {
                    return wall;
                }
            }

            return null;
        }

        public static NotMoveThing isCollideSteel(Rectangle r)
        {
            foreach (NotMoveThing steel in steelList)
            {
                if (steel.getRectangle().IntersectsWith(r))
                {
                    return steel;
                }
            }
            return null;
        }

        public static bool isCollideBoss(Rectangle r)
        {
            if (Boss.getRectangle().IntersectsWith(r)) return true;
            return false;
        }

        public static EnemyTank isCollideEnemyTank(Rectangle rt)
        {
            foreach(EnemyTank tank in tankList)
            {
                if (tank.getRectangle().IntersectsWith(rt))
                {
                    return tank;
                }
            }

            return null;
        }

        public static MyTank isCollideMytank(Rectangle rt)
        {
            return myTank.getRectangle().IntersectsWith(rt) ? myTank:null;
        }

        public static void CreateMap()
        {
            CreateWall(1, 1, 5, Resources.wall, wallList);
            CreateWall(3, 1, 5, Resources.wall, wallList);
            CreateWall(5, 1, 4, Resources.wall, wallList);
            CreateWall(7, 1, 3, Resources.wall, wallList);
            CreateWall(9, 1, 4, Resources.wall, wallList);
            CreateWall(11, 1, 5, Resources.wall, wallList);
            CreateWall(13, 1, 5, Resources.wall, wallList);

            CreateWall(7, 5, 1, Resources.steel, steelList);

            CreateWall(0, 7, 1, Resources.steel, steelList);

            CreateWall(2, 7, 1, Resources.wall, wallList);
            CreateWall(3, 7, 1, Resources.wall, wallList);
            CreateWall(4, 7, 1, Resources.wall, wallList);
            CreateWall(6, 7, 1, Resources.wall, wallList);
            CreateWall(7, 6, 2, Resources.wall, wallList);
            CreateWall(8, 7, 1, Resources.wall, wallList);
            CreateWall(10, 7, 1, Resources.wall, wallList);
            CreateWall(11, 7, 1, Resources.wall, wallList);
            CreateWall(12, 7, 1, Resources.wall, wallList);

            CreateWall(14, 7, 1, Resources.steel, steelList);

            CreateWall(1, 9, 5, Resources.wall, wallList);
            CreateWall(3, 9, 5, Resources.wall, wallList);
            CreateWall(5, 9, 3, Resources.wall, wallList);

            CreateWall(6, 10, 1, Resources.wall, wallList);
            CreateWall(7, 10, 2, Resources.wall, wallList);
            CreateWall(8, 10, 1, Resources.wall, wallList);

            CreateWall(9, 9, 3, Resources.wall, wallList);
            CreateWall(11, 9, 5, Resources.wall, wallList);
            CreateWall(13, 9, 5, Resources.wall, wallList);


            CreateWall(6, 13, 2, Resources.wall, wallList);
            CreateWall(7, 13, 1, Resources.wall, wallList);
            CreateWall(8, 13, 2, Resources.wall, wallList);

            CreateBoss(7, 14, Resources.Boss);
        }

        public static void createMyTank()
        {
            int x = 5 * 30;
            int y = 14 * 30;
            myTank = new MyTank(x,y,2);

        }

        private static void CreateWall(int x,int y,int count, Image img,List<NotMoveThing> wallList)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;

            for(int i= yPosition; i < yPosition + count * 30; i += 15)
            {
                NotMoveThing wall1 = new NotMoveThing(xPosition, i, img);
                NotMoveThing wall2 = new NotMoveThing(xPosition+15,i,img);
                wallList.Add(wall1);
                wallList.Add(wall2);
            }
        }

        private static void CreateBoss(int x,int y,Image img)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            Boss = new NotMoveThing(xPosition, yPosition, img);
            
        }

        public static void keyDown(KeyEventArgs args)
        {
            myTank.keyDown(args);
        }

        public static void keyUp(KeyEventArgs args)
        {
            myTank.keyUp(args);
        }
    }
}
