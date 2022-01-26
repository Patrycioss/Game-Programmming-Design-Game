namespace GXPEngine.Entities
{
    public class Entity : AnimationSprite
    {
        //Base class of all entities
        
        //Stats
        public int maxHealth;
        protected float speed;
        

        protected Entity(string filePath, int columns, int rows, int frames, bool addCollider = true) : base(filePath, columns, rows, frames, true, addCollider)
        {
            //This delayconstant is used by most entities
            int delayConstant = 120;

            //Calculates the amount of animationdelay needed based on the constant and the delta time
            if (Time.deltaTime > 1)
            {
                _animationDelay = (byte)(delayConstant / Time.deltaTime);
            }
            else _animationDelay = 40;
        }

        public virtual void Update(){}


        /// <summary>
        /// Damages this entity for a certain amount of its health
        /// </summary>
        public override void Damage(int amount)
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
        public override void AddHealth(int amount)
        {
            health += amount;
            if (health > maxHealth) health = maxHealth;
        }

        /// <summary>
        /// Kills this entity
        /// </summary>
        public override void Kill()
        {
            LateDestroy();
        }
    }
}