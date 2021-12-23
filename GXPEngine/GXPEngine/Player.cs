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
        //Vectors for calculation of movement
        public Vector2 acceleration;

        public int speed;
        
        //In what layer is the player currently?
        private int currentLayer, nextLayer;
        
       public Player(float startX, float startY, int layer) : base("new_barry_big.png", 5, 1, 7, true, true)
        {
            x = startX;
            y = startY;
            acceleration = new Vector2(0, 0);
            currentLayer = layer;
            nextLayer = layer;
            speed = 3;

            
            
        }

        void Update()
        {
            acceleration.Set(GetHorizontalMovement(),GetVerticalMovement());
            acceleration.Multiply(speed);

            
            
            
            
            
            
            
            Collision collision = MoveUntilCollision(acceleration.x, acceleration.y);
            
            if (collision != null )
            {
            
                
                if ((TimeOfImpact(collision.other, acceleration.x, acceleration.y, out collision.normal) >= 1) || !(SceneManager.currentScene.ObjectIsInLayer(collision.other, currentLayer)) )
                {
                    Move(acceleration.x,acceleration.y);
                }
            }


                acceleration.Set(0,0);

                Console.WriteLine(GetLayer(Input.mouseY));
            
            UpdateLayer();
            Console.WriteLine(currentLayer);
            // Console.WriteLine(GetCollisions()[0]);
        }
        

        
        void UpdateLayer()
        {
            currentLayer = nextLayer;

            nextLayer = GetLayer(y);
            if (nextLayer != currentLayer)
            {
                SceneManager.currentScene.MoveFromLayerToOther(name,currentLayer,nextLayer);
            }
            



            // for (int i = SceneManager.currentScene.layers.Capacity -1; i >= 0; i--)
            // {
            //     int limit1 = SceneManager.currentScene.verticalGap * i;
            //     int limit2 = limit1 + SceneManager.currentScene.verticalGap;
            //     
            //     if (bottomY >= limit1 && bottomY < limit2)
            //     {
            //         if (i != currentLayer)
            //         {
            //             SceneManager.currentScene.MoveFromLayerToOther(name,currentLayer,i);
            //             currentLayer = i;
            //             break;
            //         }
            //     }
            // }
        }

        int GetLayer(float y)
        {
            return (int)Extra.Remap(y, game.height/2, game.height, 0, SceneManager.currentScene.layers.Count);
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
    }
}