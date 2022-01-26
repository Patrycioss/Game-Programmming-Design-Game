
using GXPEngine.Core;

namespace GXPEngine.Entities.Enemies
{
    /// <summary>
    /// Base class for basic enemy ai (move until it hits something then moves the other way)
    /// </summary>
    public class Basic : Enemy
    {
        //Base class for basic enemy ai (move until it hits something then turns around)
        protected Basic(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames){}

        public override void Update()
        {
            Collision collision = MoveUntilCollision(speed * Time.deltaTime,0);
            
            if (collision != null)
            {
                speed = -speed;
                _mirrorX = (speed < 0 );
            }
            Animate();
        }
    }
    
    /// <summary>
    /// A green slime that is sliming on the ground
    /// </summary>
    public class GreenSlime : Basic
    {
        public GreenSlime() : base("sprites/enemies/green_slime2.png", 2, 1, 2)
        {
            speed = 0.2f;
            attackDamage = 1;
            health = 1;
        }
    }

    /// <summary>
    /// A flying enemy
    /// </summary>
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