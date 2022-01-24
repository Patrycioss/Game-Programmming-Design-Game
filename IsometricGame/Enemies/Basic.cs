﻿using System;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine.Enemies
{
    public class Basic : Enemy
    {
        
        public Basic(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            collider.isTrigger = true;
        }

        public override void Update()
        {
            _hCollision = MoveUntilCollision(speed * Time.deltaTime,0);

            if (HitTest(_myGame.player))
            {
                _myGame.player.Damage(attackDamage);
            }
            

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
            
            Animate();

        }

        public override void Kill()
        {
            ParticleGenerator deathParticle = new ParticleGenerator(Color.LimeGreen, 1000);
            LateDestroy();
        }
        
    }

    public class GreenSlime : Basic
    {
        public GreenSlime() : base("sprites/enemies/green_slime2.png", 2, 1, 2)
        {
            speed = 0.2f;
            attackDamage = 1;
            health = 1;
        }

    }

    public class Flyer : Basic
    {
        public Flyer() : base("sprites/enemies/flying2.png", 2, 1, 2)
        {
            speed = 0.2f;
            attackDamage = 1;
            health = 2; 
        }
    }
    
}