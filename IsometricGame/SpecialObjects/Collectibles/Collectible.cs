namespace GXPEngine.SpecialObjects.Collectibles
{
    /// <summary>
    /// Base class for all collectible objects
    /// </summary>
    public class Collectible : AnimationSprite
    {
        protected Sound pickupSound;

        protected Collectible(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames, true)
        {
            collider.isTrigger = true;
        }

        protected void Update()
        {
            if (DistanceTo(myGame.player) < 32)
            {
                Action();
                this.Destroy();
            }

            AnimateFixed();
        }
        
        /// <summary>
        /// In the action function it is specified what happens when you pick a collectible up
        /// </summary>
        protected virtual void Action(){}
    }

    /// <summary>
    /// A class for a coin which you can collect
    /// </summary>
    public class Coin : Collectible
    {
        public Coin() : base("Sprites/collectibles/coin/coin.png", 1, 1, 1)
        {
            pickupSound = new Sound("sounds/Coin.wav");
        }
        
        protected override void Action()
        {
            myGame.hud.AddCoinAmount(1);
            pickupSound.Play(volume:0.3f);
        }
    }

    /// <summary>
    /// A class for a heart which you can pick up to gain health
    /// </summary>
    public class Heart : Collectible
    {
        public Heart() : base("sprites/collectibles/heart/heart_small.png", 1, 1, 1)
        {
            collider.isTrigger = true;

            pickupSound = new Sound("sounds/heart.wav");
        }
        
        protected override void Action()
        {
            pickupSound.Play(volume:1.5f);
            
            myGame.player.AddHealth(1);
        }
    }
   
    
    
}