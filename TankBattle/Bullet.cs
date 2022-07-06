using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Properties;

namespace TankBattle
{
    enum Tag
    {
        mytank,
        enemytank
    }
    class Bullet:MoveThing
    {
        public Tag tag { get; set; }

        public bool isDestroy { get; set; }

        public Bullet(int x, int y, int speed, Direction dir,Tag tag)
        {
            isDestroy = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = Resources.BulletDown;
            BitmapUp = Resources.BulletUp;
            BitmapLeft = Resources.BulletLeft;
            BitmapRight = Resources.BulletRight;
            this.Dir = dir;
            this.tag = tag;
            this.X -= Width / 2;
            this.Y -= Height / 2;
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            base.Update();

        }

        private void MoveCheck()
        {
            //check whether exceed the edges
            if (Dir == Direction.Up)
            {
                if (Y + Height/2 +3 < 0)
                {
                    
                    isDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Height/2 -3  > 450)
                {
                    
                    isDestroy = true;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X + Width/2 -3 < 0)
                {
                    isDestroy=true;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Width/2 +3 > 450)
                {
                    isDestroy = true;
                    return;
                }
            }

            //collision
            Rectangle r = getRectangle();

            r.X = X + Width / 2 - 3;
            r.Y = Y + Height / 2 - 3;
            r.Height = 3;
            r.Width = 3;

            switch (Dir)
            {
                case Direction.Up:
                    r.Y -= Speed;
                    break;
                case Direction.Down:
                    r.Y += Speed;
                    break;
                case Direction.Left:
                    r.X -= Speed; ;
                    break;
                case Direction.Right:
                    r.X += Speed;
                    break;
            }

            //1 wall
            //2 steel
            //3 tank

            int xExp = this.X + Width / 2;
            int yExp = this.Y + Height / 2;
            NotMoveThing wall = null;
            if ((wall=GameObjectManager.isCollideWall(r))!=null)
            {
                isDestroy = true;
                GameObjectManager.DestroyWall(wall);
                GameObjectManager.CreateExplosion(xExp, yExp);
                SoundManager.Playblast();
                return;
            }
            if (GameObjectManager.isCollideSteel(r)!=null)
            {

                isDestroy = true;
                GameObjectManager.CreateExplosion(xExp, yExp);
                return;
            }
            if (GameObjectManager.isCollideBoss(r))
            {
                GameFramework.ChangeToGameOver();
                SoundManager.Playblast();
                return;
            }

            if(tag == Tag.mytank)
            {
                EnemyTank tank = null;
                if ((tank = GameObjectManager.isCollideEnemyTank(r)) != null)
                {
                    isDestroy = true;
                    GameObjectManager.Destroytank(tank);
                    GameObjectManager.CreateExplosion(xExp, yExp);
                    SoundManager.Playblast();
                    ScoreBoard.Score++;
                    return;
                }
            }else if(tag == Tag.enemytank)
            {
                MyTank tank = null;
                if((tank = GameObjectManager.isCollideMytank(r)) != null)
                {
                    isDestroy = true;
                    GameObjectManager.CreateExplosion(xExp, yExp);
                    SoundManager.Playhit();
                    tank.TakeDamage();

                    return;
                }
            }


        }

        private void Move()
        {


            if (Dir == Direction.Up)
            {
                Y -= Speed;
            }
            else if (Dir == Direction.Down)
            {
                Y += Speed;
            }
            else if (Dir == Direction.Left)
            {
                X -= Speed;
            }
            else if (Dir == Direction.Right)
            {
                X += Speed;
            }

        }
    }
}
