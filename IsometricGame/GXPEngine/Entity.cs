namespace GXPEngine
{
    public class Entity : AnimationSprite
    {
        public int maxHealth;
        public float speed;

        public Sound deathSound;
        public Sound moveSound;
        public Sound attackSound;

        public Entity(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames, true, true){}

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