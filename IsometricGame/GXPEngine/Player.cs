using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Threading;
using System.Xml.Schema;
using GXPEngine.Core;
using GXPEngine.Enemies;
using Microsoft.Win32.SafeHandles;
using TiledMapParser;

namespace GXPEngine
{
    public class Player : Entity 
    {
        //PlayerInfo
        public State currentState;

        public Vector2 velocity;
        
        
        public Vector2 center;

        private bool wasGrounded;
        private bool isGrounded;


        public bool mirrored;

        public Powerup currentPowerup;

        //Collision
        private Collision verticalCollision;
        private Collision horizontalCollision;

        //Constants    
        private float gravitationalForce;

        private float baseSpeed;
        private float runSpeed;
        private float movementSpeed;
        
        protected Collision _collision;


        //JUMP
        private float jumpForce;

        public Timer immunityTimer;
        public int immunityDuration;

        public Player() : base("sprites/player/player.png", 3, 1, 3)
        {
            //Positional info
            // x = startX;
            // y = startY;
            center = new Vector2(x + width / 2, y + height / 2);

            //Stats
            attackDamage = 1;
            health = 3;
            maxHealth = 3;
            
            //Starting state
            currentState = State.Stand;

            //Constants    
            immunityDuration = 2000;

            //Movement
            baseSpeed = 0.4f;
            runSpeed = 0.5f;
            movementSpeed = baseSpeed;
            
            //Jumping
            jumpForce = 1.4f;

            //Gravity
            gravitationalForce = 0.005f;

            //Animation
            SetCycle(0, 2,switchFrame:true);
            
        }

        
        

        void UpdateInformation()
        {
            center.Set(x + width / 2, y + height / 2);
            wasGrounded = isGrounded;

            if (_mirrorX)
            {
                mirrored = true;
            }
            else mirrored = false;
        }


        public override void Update()
        {
            UpdateInformation();
            CheckIfGrounded();
            Animate();


            Console.WriteLine(isGrounded);
            
            //Immunityframe
            if (immunityTimer != null)
            {
                if (!immunityTimer.finished)
                {
                    alpha = Utils.Random(0.4f, 1);
                    movementSpeed = runSpeed;
                }
                else
                {
                    alpha = 1;
                    movementSpeed = baseSpeed;

                    if (collider.isTrigger)
                    {
                        collider.isTrigger = false;

                    }
                }
            }
            
            //PowerMove
            if (Input.GetKey(Key.F) && currentPowerup != null)
            {
                currentPowerup.Use();
            }
            
            //Move the player and store its vertical and horizontal collision
            verticalCollision = MoveUntilCollision(0, velocity.y * Time.deltaTime); 
            horizontalCollision = MoveUntilCollision(velocity.x * Time.deltaTime, 0);

                //STATES//
                //This switch statement switches according to 1 of 3 states: Stand, Walk or Jump//
                //Jump: player is in the air//
                //Stand: player stands still//
                //Walk: player is walking   //
                switch (currentState)
                {
                    case State.Stand:

                        SetCycle(0,1,switchFrame: true);
                        
                        velocity.Set(0, 0);
                        
                        if (!isGrounded)
                        {
                            currentState = State.Jump;
                            break;
                        }

                        if (Input.GetKey(Key.A) != Input.GetKey(Key.D))
                        {
                            currentState = State.Walk;
                            break;
                        }

                        else if (Input.GetKey(Key.SPACE))
                        {
                            Jump();
                            currentState = State.Jump;
                            break;
                        }

                        break;

                    case State.Walk:
                        
                        SetCycle(0,2,switchFrame: true);

                        
                        if (Input.GetKey(Key.A) == Input.GetKey(Key.D))
                        {
                            currentState = State.Stand;
                            break;
                        }

                        else if (Input.GetKey(Key.D))
                        {
                            velocity.x = movementSpeed;
                            Mirror(false, false);
                        }
                        else if (Input.GetKey(Key.A))
                        {
                            velocity.x = -movementSpeed;
                            Mirror(true, false);
                        }

                        if (Input.GetKey(Key.SPACE))
                        {
                            Jump();
                            currentState = State.Jump;
                            break;

                        }
                        //Coyote time by also having wasGrounded to be false
                        else if (!isGrounded && !wasGrounded)
                        {
                            currentState = State.Jump;
                            break;
                        }

                        break;

                    case State.Jump:
                        
                        velocity.y += Time.deltaTime * gravitationalForce;
                            

                        if (Input.GetKey(Key.D) == Input.GetKey(Key.A))
                        {
                            velocity.x = 0.0f;
                        }
                        else if (Input.GetKey(Key.D))
                        {
                            velocity.x = movementSpeed;
                            Mirror(false, false);
                 
                        }
                        else if (Input.GetKey(Key.A))
                        {
                            velocity.x = -movementSpeed;
                            Mirror(true, false);
                        }

                        if (isGrounded && (Input.GetKey(Key.A) != Input.GetKey(Key.D)))
                        {
                            velocity.y = 0;
                            currentState = State.Walk;
                            break;
                        }
                        else if (isGrounded)
                        {
                            velocity.y = 0;
                            currentState = State.Stand;
                            break;
                        }

                        break;
                }
            }
        

        public enum State
        {
            Stand,
            Walk,
            Jump
        }

        void Jump()
        {
            //TODO: Jump sound
            velocity.y -= jumpForce;
        }

        public void CheckIfGrounded()
       {
           if (verticalCollision != null)
           {
               if (verticalCollision.other.y > y)
               {
                   isGrounded = true;
               }
               else velocity.y = 0;
           }
            else isGrounded = false;
       }
        public override void Damage(int amount)
        {
            if (amount > 0)
            {
                if (immunityTimer == null || immunityTimer.finished)
                {
                    health -= amount;

                    _myGame.hud.RemoveHeart();
                
                    if (health <= 0)
                    {
                        Kill();
                    
                    }
                    else
                    {
                        immunityTimer = new Timer(immunityDuration);
                        collider.isTrigger = true;
                    }
                }
            }
        }

        public override void Kill()
        {
            _myGame.AddChild(_myGame.menu);
            _myGame.RemoveChild(_myGame.StageLoader);
            _myGame.RemoveChild(_myGame.hud);
            _myGame.StageLoader.Clear();
            Reset();
        }

        public override void AddHealth(int amount)
        {
            health += amount;
            _myGame.hud.AddHealth(amount);
        }

        public void Reset()
        {
            health = maxHealth;
            currentPowerup = null;
            Mirror(false,false);
        }
    }
}