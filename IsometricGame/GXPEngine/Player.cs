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

        public Vector2 speed;
        
        
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
        private Timer jumpAnimationTimer;
        private int jumpAnimationDuration;

        public Timer immunityTimer;
        public int immunityDuration;

        public Player(float startX, float startY) : base("sprites/player/player.png", 3, 1, 3)
        {
            //Positional info
            x = startX;
            y = startY;
            center = new Vector2(x + width / 2, y + height / 2);

            //Stats
            attackDamage = 1;
            health = 3;
            
            //Starting state
            currentState = State.Stand;

            //Constants    
            immunityDuration = 2000;

            //Movement
            baseSpeed = 0.3f;
            runSpeed = 0.5f;
            movementSpeed = baseSpeed;
            
            //Jumping
            jumpForce = 0.8f;
            jumpAnimationDuration = 2;

            //Gravity
            gravitationalForce = 0.012f;

            //Animation
            SetCycle(0, 2, Byte.MaxValue, true);
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
            AnimateFixed();
            
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
            verticalCollision = MoveUntilCollision(0, speed.y * Time.deltaTime); 
            horizontalCollision = MoveUntilCollision(speed.x * Time.deltaTime, 0);

                //STATES//
                //This switch statement switches according to 1 of 3 states: Stand, Walk or Jump//
                //Jump: player is in the air//
                //Stand: player stands still//
                //Walk: player is walking   //
                switch (currentState)
                {
                    case State.Stand:

                        SetCycle(0,1,switchFrame: true);
                        
                        speed.Set(0, 0);
                        
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

                            //TODO: Play Jump Audio

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
                            speed.x = movementSpeed;
                            Mirror(false, false);
                        }
                        else if (Input.GetKey(Key.A))
                        {
                            speed.x = -movementSpeed;
                            Mirror(true, false);
                        }

                        if (Input.GetKey(Key.SPACE))
                        {
                            Jump();

                            //TODO: Play Jump Audio

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
                        
                        speed.y += gravitationalForce;
                            

                        if (Input.GetKey(Key.D) == Input.GetKey(Key.A))
                        {
                            speed.x = 0.0f;
                        }
                        else if (Input.GetKey(Key.D))
                        {
                            speed.x = movementSpeed;
                            Mirror(false, false);
                 
                        }
                        else if (Input.GetKey(Key.A))
                        {
                            speed.x = -movementSpeed;
                            Mirror(true, false);
                        }

                        if (isGrounded && (Input.GetKey(Key.A) != Input.GetKey(Key.D)))
                        {
                            speed.y = 0;
                            currentState = State.Walk;
                            break;
                        }
                        else if (isGrounded)
                        {
                            speed.y = 0;
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
            if (jumpAnimationTimer == null)
            {
                jumpAnimationTimer = new Timer(jumpAnimationDuration);
            }

            if (!jumpAnimationTimer.finished)
            {
                speed.y -= jumpForce;
            }
            else jumpAnimationTimer = null;
        }

        public void CheckIfGrounded()
       {
           if (verticalCollision != null)
            {
                isGrounded = true;
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
            AddHealth(3);
        }

        public override void AddHealth(int amount)
        {
            health += amount;
            _myGame.hud.AddHealth(amount);
        }
    }
}