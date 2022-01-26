using GXPEngine.Core;
using GXPEngine.StageManagement;
using GXPEngine.UserInterface;

namespace GXPEngine.SpecialObjects
{
    public class Finish : Sprite
    {
        private Vector2 destination;
        public Finish(string imagePath, float x, float y) : base(imagePath, addCollider: true)
        {
            this.x = x;
            this.y = y;
            
            myGame = (MyGame) game;

            collider.isTrigger = true;

        }

        public void SetPlayerDestination( float newX, float newY)
        {
            destination.Set(newX,newY);
        }

        void Update()
        {
            if (HitTest(myGame.player))
            {
                
                myGame.RemoveChild(myGame.hud);
                
                myGame.player.Reset();

                StageLoader.ClearStage();
                    
                
                myGame.hud = new Hud();
                
               myGame.AddChild(myGame.menu);
            }
        }
    }
}