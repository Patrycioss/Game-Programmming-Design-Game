namespace GXPEngine
{
    public class Entity : AnimationSprite
    {
        protected int delayConstant;
        public int maxHealth;
        public float speed;

        public Sound deathSound;
        public Sound moveSound;
        public Sound attackSound;

        public Entity(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames, true, true)
        {
            delayConstant = 120;
            
            if (Time.deltaTime > 1)
            {
                _animationDelay = (byte)(delayConstant / Time.deltaTime);
            }
            else _animationDelay = 40; 
            
        }

        public virtual void Update(){}


        public override void Damage(int amount)
        {
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