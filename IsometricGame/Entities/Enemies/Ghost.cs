﻿using GXPEngine.Core;
using GXPEngine.Extras;

namespace GXPEngine.Entities.Enemies
{
    /// <summary>
    /// A ghost is an enemy that tracks the player
    /// </summary>
    public class Ghost : Enemy
    {
        private Vector2 desiredPosition;
        private Vector2 direction;
        protected int detectionRadius;
        protected bool canGoThroughWalls;
        protected bool playerSpotted;

        protected Ghost(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            SetCycle(0,2);
            _animationDelay = 200;
            
            //desiredPosition is the position of the player
            desiredPosition = new Vector2(myGame.player.x, myGame.player.y);
            sound = new Sound("sounds/ghost.ogg");
            volume = 0.2f;

            playerSpotted = false;

            visible = false;
        }

        public override void Update()
        {
            desiredPosition.Set(myGame.player.x, myGame.player.y);
            

            //Calculate direction
            direction = Mathf.Subtract(desiredPosition, new Vector2(x,y));


            if (direction.Magnitude() < detectionRadius)
            {
                SpotPlayer();
            }

            //Move object when player has been spotted
            if (playerSpotted)
            {
                visible = true;
                direction.Normalize();
                direction.Multiply(speed);

                if (canGoThroughWalls) Move(Time.deltaTime * direction.x, Time.deltaTime * direction.y);
                else MoveUntilCollision(Time.deltaTime * direction.x, Time.deltaTime * direction.y);
            }
            
            //Mirror if necessary
            _mirrorX = (direction.x < 0);
            
            Animate(Time.deltaTime);
        }
        
        private void SpotPlayer()
        {
            if (!playerSpotted)
            {
                playerSpotted = true;
                PlaySound();
            }
        }
    }
    
    

    public class RedGhost : Ghost
    {
        /// <summary>
        /// A RedGhost can't move through blocks and has a lower detection radius
        /// but has more health and is faster
        /// </summary>
        public RedGhost() : base("sprites/enemies/redGhost2.png", 2, 1, 2)
        {
            attackDamage = 1;
            speed = 0.2f;
            health = 2;
            detectionRadius = 300;
            canGoThroughWalls = true;
            collider.isTrigger = true;
        }
    }

    /// <summary>
    /// A BlueGhost can move through blocks and has a higher detection radius
    /// but has less health and is slower
    /// </summary>
    public class BlueGhost : Ghost
    {
        public BlueGhost() : base("sprites/enemies/blueGhost2.png", 2, 1, 2)
        {
            attackDamage = 1;
            speed = 0.2f;
            health = 1;
            detectionRadius = 500;
            canGoThroughWalls = true;
            collider.isTrigger = true;
        }
    }
}