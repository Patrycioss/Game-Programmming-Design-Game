using System;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Finish : Sprite
    {
        private Vector2 destination;
        public Finish(string imagePath, float x, float y) : base(imagePath, false, true)
        {
            this.x = x;
            this.y = y;
            
            _myGame = (MyGame) game;

            collider.isTrigger = true;

        }

        public void SetPlayerDestination(float x, float y)
        {
            destination.Set(x,y);
        }

        void Update()
        {
            if (HitTest(_myGame.player))
            {
                
                _myGame.RemoveChild(_myGame.hud);
                
                _myGame.player.Reset();

                StageLoader.ClearStage();
                    
                
                _myGame.hud = new Hud();
                
               _myGame.AddChild(_myGame.menu);
            }
        }
    }
}