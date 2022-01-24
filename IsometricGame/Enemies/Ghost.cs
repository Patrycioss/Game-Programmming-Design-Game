using System;
using GXPEngine.Core;

namespace GXPEngine.Enemies
{
    public class Ghost : Enemy
    {
        protected Vector2 position;
        protected Vector2 desiredPosition;
        protected Vector2 direction;
        protected int detectionRadius;

        //Can fly through walls?
        protected bool ghosting;
        
        public Ghost(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            SetCycle(0,2,255);
            position = new Vector2(x, y);
            desiredPosition = new Vector2(_myGame.player.x, _myGame.player.y);
        }

        public override void Update()
        {
            //Update positional info
            position.Set(x, y);
            desiredPosition.Set(_myGame.player.x, _myGame.player.y);

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

            if (direction.x < 0)
            {
                Mirror(true, false);
            }
            else Mirror(false, false);

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