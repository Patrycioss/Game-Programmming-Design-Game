using System;
using GXPEngine.Core;

namespace GXPEngine.Enemies
{
    public class Basic : Enemy
    {
        
        public Basic(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            SetCycle(0,2, animationDelay: Byte.MaxValue);
        }

        public override void Update()
        {
            _hCollision = MoveUntilCollision(speed,0);
            

            if (_hCollision != null)
            {
                if (_hCollision.other == _myGame.player)
                {
                    _myGame.player.Damage(attackDamage);
                }
                else
                {
                    speed = -speed;
                    if (speed < 0)
                    {
                        Mirror(true,false);
                    }
                    else Mirror(false,false);
                } 
            } 
            
            AnimateFixed();

        }
    }

    public class GreenSlime : Basic
    {
        public GreenSlime() : base("sprites/enemies/green_slime2.png", 2, 1, 2)
        {
            speed = 1;
            attackDamage = 1;
            health = 1;
        }

    }

    public class Flyer : Basic
    {
        public Flyer() : base("sprites/enemies/flying2.png", 2, 1, 2)
        {
            speed = 1;
            attackDamage = 1;
            health = 2;
        }
    }
    
}