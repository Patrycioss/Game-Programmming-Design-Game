﻿using GXPEngine.Core;
using GXPEngine.Entities.Enemies;

namespace GXPEngine.Entities
{
    public class FireBullet : Entity
    {
        public FireBullet(float speed, int damage, bool mirrored) : base("sprites/collectibles/powerups/fire_bullet.png", 4, 1, 4)
        {
            //Determines how far away from the player bullets spawn
            //setting this too low may cause bullets to disappear immediately and/or damaging the player
            int bulletOffset = 10;

            //Is a trigger so you can't jump on top of the bullet
            collider.isTrigger = true;

            SetScaleXY(2);
            attackDamage = damage;

            
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

            Animate();

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
                    gameObject.Damage(attackDamage);
                    Kill();

                    break;
                }
            }

            //If the bullet is offScreen it kills itself for performance
            if (DistanceTo(myGame.player) > myGame.width)
            {
                Kill();
            }
        }
    }
            
}