using GXPEngine.Core;
using GXPEngine.Entities.Enemies;

namespace GXPEngine.Entities
{
    public class FireBullet : Entity
    { 
        private Collision collision;

        public FireBullet(float speed, int damage, bool mirrored) : base("sprites/collectibles/powerups/fire_bullet.png", 4, 1, 4)
        {
            int bulletOffset = 10;

            collider.isTrigger = true;

            SetScaleXY(2);

            attackDamage = damage;

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
            collision = MoveUntilCollision(speed * Time.deltaTime, 0, true);

            Animate();

            if (collision != null)
            {
                if (collision.other != myGame.player)
                {
                    collision.other.Damage(attackDamage);
                }

                Kill();
            }

            foreach (GameObject gameObject in GetCollisions())
            {
                if (gameObject is Enemy)
                {
                    gameObject.Damage(attackDamage);
                    Kill();

                    break;
                }
            }

            if (DistanceTo(myGame.player) > myGame.width)
            {
                Kill();
            }
        }
    }
            
}