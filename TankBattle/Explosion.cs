﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Properties;

namespace TankBattle
{
    class Explosion : GameObject
    {
        public bool isNeedDestroy { get; set; }
        private int playSpeed = 1;
        private int playCount =0;
        private int index = 0;
        private Bitmap[] bmpArray = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5,
        };
        public Explosion(int x,int y)
        {
            isNeedDestroy = false;
            foreach(Bitmap bmp in bmpArray)
            {
                bmp.MakeTransparent(Color.Black);
            }

            this.X = x - bmpArray[0].Width / 2;
            this.Y = y - bmpArray[0].Height / 2;
        }
        protected override Image GetImage()
        {
            if (index > 4) return bmpArray[4];
            return bmpArray[index];
        }

        public override void Update()
        {
            playCount++;
            index = (playCount - 1) / playSpeed;
            if(index > 4)
            {
                isNeedDestroy = true;
            }
            base.Update();
        }
    }
}
