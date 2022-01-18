using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GXPEngine.Core;
using GXPEngine.Enemies;
using GXPEngine.Managers;

namespace GXPEngine
{
    public class Powerup : Collectible
    {
        protected Timer useTimer;
        public Powerup(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
        }

        protected override void Action()
        {
            _myGame.player.currentPowerup = this;
        }

        public virtual void Use()
        {
            
        }
    }

    public class FireBulletShooter : Powerup
    {

        public int bulletSpeed;
        public int coolDown;
        public int damage;
        private FireBullet _fireBullet;
        
        public FireBulletShooter() : base("sprites/collectibles/powerups/fire_pickup_big.png", 4, 1, 4)
        {
            SetXY(_myGame.player.x,_myGame.player.y);
            visible = true;

            coolDown = 1000;
            bulletSpeed = 3;
            damage = 1;
        }

        public override void Use()
        {
            if (useTimer == null || useTimer.finished)
            {
                useTimer = new Timer(coolDown);
                
                _fireBullet = new FireBullet(bulletSpeed, damage,_myGame.player.mirrored);
                _myGame.StageLoader.stageContainer.AddChild(_fireBullet);
            }
        }
        
    }

    public class FireBullet : Entity
    {
        private Collision _collision;
        
        public int bulletOffset;
        
        public FireBullet(int speed, int damage, bool mirrored) : base("sprites/collectibles/powerups/fire_bullet.png", 4, 1, 4)
        {
            collider.isTrigger = true;
            
            SetScaleXY(2);

            bulletOffset = 5;
            attackDamage = damage;
            
            if (mirrored)
            {
                Mirror(true, false);
                SetXY(_myGame.player.x - bulletOffset - _myGame.player.width, _myGame.player.y);
                this.speed = -speed;
            }
            else
            {
                this.speed = speed;
                SetXY(_myGame.player.x + _myGame.player.width + bulletOffset, _myGame.player.y);

            } 
        }

        public override void Update()
        {
            _collision = MoveUntilCollision(speed, 0);
            AnimateFixed();

            if (_collision != null)
            {
                if (_collision.other != _myGame.player)
                {
                    _collision.other.Damage(attackDamage);
                }
                Kill();
            }
            
            

            if (DistanceTo(_myGame.player) > _myGame.width)
            {
                Kill();
            }
        }
    }
}