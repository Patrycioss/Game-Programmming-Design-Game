using System;
using System.Xml.Schema;
using GXPEngine.Managers;

namespace GXPEngine
{
    public class Collectible : AnimationSprite
    {
        protected Sound sound;

        public Collectible(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames, true, true)
        {
            collider.isTrigger = true;
        }

        protected void Update()
        { 
            if (DistanceTo(_myGame.player) < 32)
                {
                    Action();
                    this.Destroy();
                }

            AnimateFixed();
        }
        
        protected virtual void Action(){}
    }

    public class Coin : Collectible
    {
        public Coin() : base("Sprites/collectibles/coin/coin.png", 1, 1, 1)
        {
            sound = new Sound("sounds/Coin.wav");
        }
        
        protected override void Action()
        {
            _myGame.hud.AddCoinAmount(1);
            sound.Play(volume:0.3f);
        }
    }

    public class Heart : Collectible
    {
        public Heart() : base("sprites/collectibles/heart/heart_small.png", 1, 1, 1)
        {
            collider.isTrigger = true;

            sound = new Sound("heart.wav");
        }

        protected override void Action()
        {
            sound.Play(volume:2.0f);
            
            _myGame.hud.AddHeart();
            _myGame.player.AddHealth(1);
        }
    }
   
    
    
}