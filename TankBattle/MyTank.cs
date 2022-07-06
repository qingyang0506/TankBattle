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
     class MyTank:MoveThing
    {
        public bool isMoving { get; set; }
        public int HP { get; set; }
        private int originalX;
        private int originalY;
        public MyTank(int x,int y,int speed,Direction dir = Direction.Up)
        {
            isMoving = false;
            this.X = x;
            this.Y = y;
            originalX = x;
            originalY = y;
            this.Speed = speed;
            BitmapDown = Resources.MyTankDown;
            BitmapUp = Resources.MyTankUp; 
            BitmapLeft = Resources.MyTankLeft; 
            BitmapRight = Resources.MyTankRight;
            this.Dir = dir;
            HP = 4;
        }

        public  void keyDown(KeyEventArgs args)
        {
           if(args.KeyCode == Keys.W || args.KeyCode == Keys.Up)
            {
                Dir = Direction.Up;
                isMoving = true;
            }
            else if(args.KeyCode == Keys.Down || args.KeyCode == Keys.S)
            {
                Dir = Direction.Down;
                isMoving = true;
            }
            else if(args.KeyCode == Keys.Left || args.KeyCode == Keys.A)
            {
                Dir = Direction.Left;
                isMoving = true;
            }
            else if(args.KeyCode == Keys.Right || args.KeyCode ==Keys.D)
            {
                Dir= Direction.Right;
                isMoving = true;
            }else if(args.KeyCode == Keys.Space)
            {
                //bullet

                attack();
            }
        }

        private  void attack()
        {
            SoundManager.Playfire();
            int x = this.X;
            int y = this.Y;

            switch (Dir)
            {
                case Direction.Up:
                    x = X + Width / 2;
                    break;
                case Direction.Down:
                    x = X+ Width / 2;
                    y = Y+ Height;
                    break;
                case Direction.Left:
                    y = Y+ Height / 2;
                    break;
                case Direction.Right:
                    x = X+ Width;
                    y = Y+ Height / 2;
                    break;
            }
            GameObjectManager.CreateBullet(x, y, Tag.mytank, Dir);
        }

        public  void keyUp(KeyEventArgs args)
        {
            if (args.KeyCode == Keys.W || args.KeyCode == Keys.Up)
            {
                isMoving = false;
            }
            else if (args.KeyCode == Keys.Down || args.KeyCode == Keys.S)
            {
                isMoving = false;
            }
            else if (args.KeyCode == Keys.Left || args.KeyCode == Keys.A)
            {
                isMoving = false;
            }
            else if (args.KeyCode == Keys.Right || args.KeyCode == Keys.D)
            {
                isMoving = false;
            }
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
                if (Y - Speed < 0)
                {
                    isMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    isMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    isMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    isMoving = false;
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
                isMoving = false;
            }
            if (GameObjectManager.isCollideSteel(r)!=null)
            {
                isMoving = false;
            }
            if (GameObjectManager.isCollideBoss(r))
            {
                isMoving = false;
            }
        }

        private void Move()
        {
            if (isMoving == false) return;

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

        public void TakeDamage()
        {
            HP--;
            if(HP <= 0)
            {
                X = originalX;
                Y = originalY;
                HP = 4;
                LifeBoard.Life--;
            }
        } 
    }
}
