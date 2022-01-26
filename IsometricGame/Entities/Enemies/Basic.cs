

namespace GXPEngine.Entities.Enemies
{
    public class Basic : Enemy
    {
        //Base class for basic enemy ai (move until it hits something then turns around)
        protected Basic(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            collider.isTrigger = true;
        }

        public override void Update()
        {
            collision = MoveUntilCollision(speed * Time.deltaTime,0);
            
            if (collision != null)
            {
                speed = -speed;
                _mirrorX = (speed < 0 );
            }
            Animate();

        }

        public override void Kill()
        {
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