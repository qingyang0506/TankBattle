using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Properties;

namespace TankBattle
{
     class EnemyTank:MoveThing
    {
        public int ChangeDirSpeed;
        private int changeDirCount = 0;
        private Random r = new Random();
        private int attackCount = 0;
        public int AttackSpeed { get; set; }
        public EnemyTank(int x, int y, int speed, Bitmap bitDown,Bitmap bitUp,Bitmap bitRight,Bitmap bitLeft)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = bitDown;
            BitmapUp = bitUp;
            BitmapLeft = bitLeft;
            BitmapRight = bitRight;
            this.Dir = Direction.Down;
            AttackSpeed = 60;
            ChangeDirSpeed = 70;
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            AttackCheck();
            AutoChangeDirection();
            base.Update();

        }

        private void AutoChangeDirection()
        {
            changeDirCount++;
            if (changeDirCount < ChangeDirSpeed) return;
            changeDirection();
            changeDirCount = 0;
        }

        private void AttackCheck()
        {
            attackCount++;
            if (attackCount < AttackSpeed) return;

            attack();
            attackCount = 0;
        }

        private void MoveCheck()
        {
            //check whether exceed the edges
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    changeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    changeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    changeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    changeDirection();
                    return;
                }
            }

            //collision
            Rectangle r = getRectangle();

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

            if (GameObjectManager.isCollideWall(r)!=null)
            {
                changeDirection();
            }
            if (GameObjectManager.isCollideSteel(r)!=null)
            {
                changeDirection();
            }
            if (GameObjectManager.isCollideBoss(r))
            {
                changeDirection();
            }


        }

        private void attack()
        {
            int x = this.X;
            int y = this.Y;

            switch (Dir)
            {
                case Direction.Up:
                    x = X + Width / 2;
                    break;
                case Direction.Down:
                    x = X + Width / 2;
                    y = Y + Height;
                    break;
                case Direction.Left:
                    y = Y + Height / 2;
                    break;
                case Direction.Right:
                    x = X + Width;
                    y = Y + Height / 2;
                    break;
            }
            GameObjectManager.CreateBullet(x, y, Tag.enemytank, Dir);
        }

        private void changeDirection()
        {
            int dir = (int)Dir;
            while(true){
                int x = r.Next(0, 4);
                if (dir != x)
                {
                    Dir = (Direction)x;
                    break;
                }
            }
            MoveCheck();
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
