using System;
using GXPEngine.Core;
using GXPEngine.Entities.Enemies;

namespace GXPEngine.Entities
{
    /// <summary>
    /// Class for FireBullets that are shot by the FireBulletShooter powerup
    /// </summary>
    public class FireBullet : Entity
    {
        private bool soundPlayed;
        
        public FireBullet(float speed, int damage, bool mirrored) : base("sprites/collectibles/powerups/fire_bullet.png", 4, 1, 4)
        {
            //Determines how far away from the player bullets spawn
            //setting this too low may cause bullets to disappear immediately and/or damaging the player
            int bulletOffset = 10;

            //Is a trigger so you can't jump on top of the bullet
            collider.isTrigger = true;

            SetScaleXY(2);
            attackDamage = damage;

            //Soundeffect
            sound = new Sound("sounds/fireBullet.ogg");
            volume = 1.0f;
            soundPlayed = false;
            
            //Changes the orientation and direction based on if it's mirrored
            if (mirrored)
            {
                Mirror(true, false);
                SetXY(myGame.player.x - 2.5f * bulletOffset - myGame.player.width, myGame.player.y + 10);
                this.speed = -speed;
            }
            else
            {
                this.speed = speed;
                SetXY(myGame.player.x + myGame.player.width + bulletOffset, myGame.player.y + 10);
            }
        }

        public override void Update()
        {
            Collision tileCollision = MoveUntilCollision(speed * Time.deltaTime, 0, true);

            Animate(Time.deltaTime);

            //Kills itself if it collides with a tile
            if (tileCollision != null)
            {
                Kill();
            }

            //If it collides with an enemy it damages the enemy and then kills itself
            foreach (GameObject gameObject in GetCollisions())
            {
                if (gameObject is Enemy)
                {
                    Enemy enemy = (Enemy) gameObject;
                    enemy.Damage(attackDamage);
                    Kill();

                    break;
                }
            }

            //If the bullet is offScreen it kills itself for performance
            if (DistanceTo(myGame.player) > myGame.width)
            {
                LateDestroy();
            }

            if (!soundPlayed)
            {
                sound.Play();
                soundPlayed = true;
            }
        }
        public override void Kill()
        {
            Sound explosion = new Sound("sounds/explosion.ogg");
            explosion.Play(volume:0.3f);
            LateDestroy();
        }
    }
            
}