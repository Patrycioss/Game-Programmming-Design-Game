using System;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using GXPEngine.Core;
using GXPEngine.GXPEngine.AddOns;
using TiledMapParser;

namespace GXPEngine
{
    public class Player : AnimationSprite
    {
        public Vector2 position, velocity, acceleration;

        private Vector2 constraintPoint1, constraintPoint2;

        private int layerRange;
        private int currentLayer;
        
        
       public Player(float startX, float startY) : base("barry_big.png", 7, 1, 7, true, true)
        {
            x = startX;
            y = startY;

            position = new Vector2(x, y);
            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);

            constraintPoint1 = new Vector2(-width/2, height);
            constraintPoint2 = new Vector2(-game.width/2 - width, game.height/2 - height);

            layerRange = 0;
            currentLayer = 0;

        }

        void Update()
        {
            position.Set(x,y);

            UpdateAcceleration();
            
            UpdateVelocity();
            acceleration.Set(0,0);
            
            UpdatePosition();

            UpdateLayer();
        }
        
        //Updates the position vector with the velocity vector and syncs
        //the vector with the x and y coordinates of the player.
        void UpdatePosition()
        {
            position.Set(x,y);
            position.x += GetHorizontalMovement();
            position.y += GetVerticalMovement();
         
            
            SetXY(position.x, position.y);

        }

        void UpdateAcceleration()
        {
            // //Calculate new acceleration
            // Vector2 newAcceleration = new Vector2(0, 0);
            // newAcceleration.Add(GetHorizontalMovement(),GetVerticalMovement());
            //
            //
            // //Calculate expected position with new acceleration
            // Vector2 expectedPosition = new Vector2(position + velocity + newAcceleration);
            //
            // //Apply the change  to the acceleration if the expected position is within the constraints
            // // if ()
            
            acceleration.Add(GetHorizontalMovement(),GetVerticalMovement());
            
        }

        void UpdateVelocity()
        {
            velocity.Add(acceleration);
            
            velocity.Divide(2);
            //
            // if (position.y + velocity.y <= yLimit)
            // {
            //     velocity.y = 0;
            // }
        }

        void UpdateLayer()
        {
            for (int i = 0; i < SceneManager.currentScene.layers.Count; i++)
            {
                int limit1 = game.height / 2 - height - i;
                int limit2 = limit1 - 32;
                
                if (y < limit1 && y >= limit2)
                {
                    SceneManager.currentScene.MoveFromLayerToOther(name,currentLayer,i);
                    break;
                }
            }
            
        }

        void SetCurrentLayer(int layer)
        {
            currentLayer = layer;
        }

        void CalcLayerRange(int layer)
        {
            layerRange = (game.height/2-height)/SceneManager.currentScene.layers.Count;
        }
        
        //Returns 1, 0 or -1 based on horizontal movement
        float GetHorizontalMovement()
        {
            if (Input.GetKey(Key.LEFT))
            {
                return -1;
            }
            else if (Input.GetKey(Key.RIGHT))
            {
                return 1;
            }
            return 0;
        }
        
        //Returns 1, 0 or -1 based on vertical movement
        float GetVerticalMovement()
        {
            if (Input.GetKey(Key.DOWN))
            {
                return 1;
            }
            else if (Input.GetKey(Key.UP))
            {
                return -1;
            }
            return 0;
        }

        // public void SetYConstraint(topY, botY)
        // {
        //     yConstraint.Set(y1 - height, y2 + height);
        // }
    }
}