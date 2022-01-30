using System.Management.Instrumentation;
using System.Runtime.DesignerServices;
using GXPEngine.Core;
using GXPEngine.Extras;
using GXPEngine.SpecialObjects.Collectibles;
using GXPEngine.StageManagement;
using GXPEngine.UserInterface;

namespace GXPEngine.Entities
{
    public class Player : Entity 
    {
        ///PLAYER INFO
        
            //Visual
            private readonly AnimationSprite sprite;
            public bool mirrored;

            //Positional
            private State currentState;
            private Vector2 velocity;
            private Vector2 center;
            private bool wasGrounded;
            private bool isGrounded;
            private Collision verticalCollision;

            //Immunity Frames
            private bool immune;
            private Timer immunityTimer;
            private readonly int immunityDuration;
            
            //Movement   
            private readonly float  jumpForce;
            private readonly float gravitationalForce;
            private readonly float runSpeed;
           
            private float baseSpeed;
            private float currentMovementSpeed;
        
            //Practical
            public Powerup currentPowerup;

        public Player() : base("sprites/player/hitbox.png", 1, 1, 1)
        {

            _animationDelay = 120;
            
            //Visual
            collider.isTrigger = true;
            alpha = 0;
            
                //Make the player model
                sprite = new AnimationSprite("sprites/player/player.png", 3, 1, 3, true, false)
                {
                    alpha = 1,
                    width = 50,
                    height = 50
                };
                AddChild(sprite);
                sprite.SetCycle(0, 2, _animationDelay);


            //Positional info
            center = new Vector2(0, 0);

            //Stats
            attackDamage = 1;
            health = 3;
            maxHealth = 3;

            //Starting state
            currentState = State.Stand;

            //Immunity    
            immunityDuration = 2000;
            immunityTimer = new Timer(0);

            //Movement
            baseSpeed = 0.4f;
            runSpeed = 0.5f;
            currentMovementSpeed = baseSpeed;

            jumpForce = 1.4f;
            gravitationalForce = 0.005f;
        }

  

        public override void Update()
        {
            UpdateInformation();
            CheckIfGrounded();
            CheckForDamageFromEnemies();
            AnimatePlayerModel();


            //Gives the player a speed boost and makes the sprite flicker when they are immune
            if (immune)
            {
                sprite.alpha = Utils.Random(0.4f, 1);
                currentMovementSpeed = runSpeed;            }
            else
            {
                sprite.alpha = 1;
                currentMovementSpeed = baseSpeed;
            }
            
            //Use currentPowerup
            if (Input.GetKey(Key.F) && currentPowerup != null)
            {
                currentPowerup.Use();
            }
            
            //Move the player and store its vertical and horizontal collision
            verticalCollision = MoveUntilCollision(0, velocity.y * Time.deltaTime); 
            MoveUntilCollision(velocity.x * Time.deltaTime, 0);

            //STATES//
            //This switch statement switches according to 1 of 3 states: Stand, Walk or Jump//
            //Jump: player is in the air//
            //Stand: player stands still//
            //Walk: player is walking   //
            switch (currentState)
            {
                case State.Stand:

                    sprite.SetCycle(0, switchFrame: true);

                    velocity.Set(0, 0);

                    if (!isGrounded)
                    {
                        currentState = State.Jump;
                        break;
                    }

                    if (Input.GetKey(Key.A) != Input.GetKey(Key.D))
                    {
                        currentState = State.Walk;
                    }

                    else if (Input.GetKey(Key.SPACE))
                    {
                        Jump();
                        currentState = State.Jump;
                    }

                    break;

                case State.Walk:

                    sprite.SetCycle(0, 2, switchFrame: true);

                    if (Input.GetKey(Key.A) == Input.GetKey(Key.D))
                    {
                        currentState = State.Stand;
                        break;
                    }

                    else if (Input.GetKey(Key.D))
                    {
                        velocity.x = currentMovementSpeed;
                        Mirror(false, false);
                    }
                    else if (Input.GetKey(Key.A))
                    {
                        velocity.x = -currentMovementSpeed;
                        Mirror(true, false);
                    }

                    if (Input.GetKey(Key.SPACE))
                    {
                        Jump();
                        currentState = State.Jump;
                    }
                    else if (!isGrounded && !wasGrounded)
                    {
                        currentState = State.Jump;
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
                        velocity.x = currentMovementSpeed;
                        Mirror(false, false);
                    }
                    else if (Input.GetKey(Key.A))
                    {
                        velocity.x = -currentMovementSpeed;
                        Mirror(true, false);
                    }

                    if (isGrounded && (Input.GetKey(Key.A) != Input.GetKey(Key.D)))
                    {
                        velocity.y = 0;
                        currentState = State.Walk;
                    }
                    else if (isGrounded)
                    {
                        velocity.y = 0;
                        currentState = State.Stand;
                    }

                    break;
            }
        }

        public override void Damage(int amount)
        {
            if (amount > 0)
            {
                //If the player has immunityframes they can't be damaged
                if (immunityTimer == null || immunityTimer.finished)
                {
                    health -= amount;

                    myGame.hud.RemoveHeart();
                
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
            //Loads up the main menu and removes the hud
            myGame.AddChild(myGame.menu);
            myGame.RemoveChild(myGame.hud);
            StageLoader.ClearStage();
            
            Reset();

            //Also resets the hud for next use
            myGame.hud = new Hud();
        }
        
        public override void AddHealth(int amount)
        {
            health += amount;
            myGame.hud.AddHealth(amount);
        }
       
        /// <summary>
        /// Resets the player to max health, removes the current powerup
        /// and faces the player to the right
        /// </summary>
        public void Reset()
        {
            health = maxHealth;
            currentPowerup = null;
            Mirror(false,false);
        }

        /// <summary>
        /// Toggles the visual hitbox on the player sprite
        /// </summary>
        public void ToggleHitBox()
        {
            alpha = -alpha + 1;
        }

        /// <summary>
        /// Overrides the currentmovementspeed with a given amount (!generally don't use this)
        /// </summary>
        public void SetBaseMovementSpeed(float amount)
        {
            baseSpeed = amount;
        }
        
        /// <summary>
        /// Updates useful information about the player
        /// </summary>
        private void UpdateInformation()
        {
            center.Set(x + width / 2.0f, y + height / 2.0f);
            wasGrounded = isGrounded;
            mirrored = _mirrorX;
            immune = (!immunityTimer.finished);
        }
        
        /// <summary>
        /// Makes the player jump :O
        /// </summary>
        private void Jump()
        {
            velocity.y -= jumpForce;
        }
        
        /// <summary>
        /// Checks if the player is grounded (standing on a block)
        /// </summary>
        private void CheckIfGrounded()
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

        /// <summary>
        /// Checks whether the player should receive damage from enemies
        /// </summary>
        private void CheckForDamageFromEnemies()
        {
            foreach (GameObject gameObject in StageLoader.GetObjects())
            {
                if (!Equals(gameObject))
                {
                    if (gameObject is Entity)
                    {
                        Entity entity = (Entity) gameObject;
                        
                        if (DistanceTo(entity) < 100)
                        {
                            if (HitTest(entity))
                            {
                                Damage(entity.attackDamage);
                                entity.PlaySound();
                                break;
                            }
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Animates the player sprite and fixes the orientation
        /// </summary>
        private void AnimatePlayerModel()
        {
            sprite.Animate(Time.deltaTime);

            if (mirrored)
            {
                sprite.Mirror(true, false);
                sprite.x = -10;
            }
            else
            {
                sprite.Mirror(false, false);
                sprite.x = -2;
            }
        }

        /// <summary>
        /// The player has three states: Stand, Walk and Jump, these states are used in movement and
        /// to determine when gravity should be applied and when certain animations
        /// should be played.
        /// </summary>
        private enum State
        {
            Stand,
            Walk,
            Jump
        }
        
    }
}