namespace GXPEngine
{
    public class Entity : AnimationSprite
    {
        //Base class of all entities
        
        //A variable that can be edited in all subclasses to ensure the animationspeed is fitting for the entity
        protected int delayConstant;
       
        //Stats
        public int maxHealth;
        public float speed;
        

        public Entity(string filePath, int columns, int rows, int frames, bool addCollider = true) : base(filePath, columns, rows, frames, true, addCollider)
        {
            //This delayconstant is used by most enities
            delayConstant = 120;
            
            //Calculates the amount of animationdelay needed based on the constant and the delta time
            if (Time.deltaTime > 1)
            {
                _animationDelay = (byte)(delayConstant / Time.deltaTime);
            }
            else _animationDelay = 40; 
            
        }

        public virtual void Update(){}


        public override void Damage(int amount)
        {
            //All entities can receive damage, this class also exists empty in GameObject so we can acccess this function when we use Hittest()
            
            health -= amount;

            if (health <= 0)
            {
                Kill();
            }
        }

        public override void AddHealth(int amount)
        {
            health += amount;
            if (health > maxHealth) health = maxHealth;
        }

        public override void Kill()
        {
            LateDestroy();
        }
    }
}