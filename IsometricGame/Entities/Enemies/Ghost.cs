using GXPEngine.Core;

namespace GXPEngine.Entities.Enemies
{
    public class Ghost : Enemy
    {
        private Vector2 position;
        private Vector2 desiredPosition;
        private Vector2 direction;
        protected int detectionRadius;

        //Can fly through walls?
        protected bool ghosting;
        
        protected Ghost(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            SetCycle(0,2);
            position = new Vector2(x, y);
            desiredPosition = new Vector2(myGame.player.x, myGame.player.y);
        }

        public override void Update()
        {
            //Update positional info
            position.Set(x, y);
            desiredPosition.Set(myGame.player.x, myGame.player.y);

            //Calculate direction
            direction = Mathf.Subtract(desiredPosition, position);

            
            //Move object when player is close
            if (direction.Magnitude() <  detectionRadius)
            {
                direction.Normalize();
                direction.Multiply(speed);
                
                if (ghosting)
                {
                    Move(Time.deltaTime * direction.x, Time.deltaTime*direction.y);
                }
                else
                {
                    MoveUntilCollision(Time.deltaTime * direction.x, Time.deltaTime * direction.y);
                }
            }

            //Mirror if necessary
            _mirrorX = (direction.x < 0);
            
            Animate();
        }
    }

    public class RedGhost : Ghost
    {
        public RedGhost() : base("sprites/enemies/redGhost2.png", 2, 1, 2)
        {
            attackDamage = 1;
            speed = 0.2f;
            health = 2;
            detectionRadius = 500;
            ghosting = false;
            collider.isTrigger = true;
        }
    }

    public class BlueGhost : Ghost
    {
        public BlueGhost() : base("sprites/enemies/blueGhost2.png", 2, 1, 2)
        {
            attackDamage = 1;
            speed = 0.1f;
            health = 1;
            detectionRadius = 1000;
            ghosting = true;
            collider.isTrigger = true;
        }
    }   
    

}