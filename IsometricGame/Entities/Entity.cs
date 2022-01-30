using System;
using GXPEngine.Extras;

namespace GXPEngine.Entities
{
    /// <summary>
    /// Base class for all entities
    /// </summary>
    public class Entity : AnimationSprite
    {
        //Stats
        public int attackDamage { get; protected set; }
        public int health { get; protected set; }

        public int maxHealth;
        protected float speed;
        
        protected Sound sound;
        protected float volume;
        protected Timer soundDelay;
        
        protected Entity(string filePath, int columns, int rows, int frames, bool addCollider = true) : base(filePath, columns, rows, frames, true, addCollider)
        {
            soundDelay = new Timer(1000, true, false);
            _animationDelay = 100;


            // //This delayconstant is used by most entities
            // int delayConstant = 120;
            //
            // //Calculates the amount of animationdelay needed based on the constant and the delta time
            // if (Time.deltaTime > 1)
            // {
            //     _animationDelay = (byte)(delayConstant / Time.deltaTime);
            //
            //     if (_animationDelay < 40) _animationDelay = 40;
            // }
            // else _animationDelay = 40;
        }

        public virtual void Update(){}


        /// <summary>
        /// Damages this entity for a certain amount of its health
        /// </summary>
        public virtual void Damage(int amount)
        {
            //All entities can receive damage, this class also exists empty in GameObject so we can access this function when we use Hittest()
            
            health -= amount;

            if (health <= 0)
            {
                Kill();
            }
        }

        /// <summary>
        /// Adds a certain amount of health to this entity's healthpool
        /// </summary>
        public virtual void AddHealth(int amount)
        {
            health += amount;
            if (health > maxHealth) health = maxHealth;
        }

        /// <summary>
        /// Kills this entity
        /// </summary>
        public virtual void Kill()
        {
            LateDestroy();
        }

        public virtual void PlaySound()
        {
            if (sound != null)
            {
                if (soundDelay.finished || soundDelay.IsPaused)
                {
                    Console.WriteLine("hoi");
                    sound.Play(volume:volume);
                    soundDelay.Reset();
                    soundDelay.IsPaused = false;
                } 
            }
            else Console.WriteLine("Tried to play sound but there isn't any!");
          
            
        }
    }
}