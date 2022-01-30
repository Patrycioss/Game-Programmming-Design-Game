using GXPEngine.Core;
using GXPEngine.Extras;
using GXPEngine.StageManagement;
using GXPEngine.UserInterface;

namespace GXPEngine.SpecialObjects
{
    /// <summary>
    /// A finish object to end a stage with
    /// </summary>
    public class Finish : Sprite
    {
        private Stages nextStage;
        private Timer timer;
        private Sound sound;
        
        public Finish(string imagePath) : base(imagePath, addCollider: true)
        {
            collider.isTrigger = true;

            sound = new Sound("sounds/victory.mp3");
            timer = new Timer(5000, true);
        }

        void Update()
        {
            if (HitTest(myGame.player))
            {
                if (timer.IsPaused)
                {
                    sound.Play();
                    timer.Reset();
                    timer.UnPause();
                    
                }

                if (timer.finished)
                {
                    myGame.menu.menuButtons[StageLoader.currentStage.stage]
                        .SetCoinsCollected(myGame.hud.coinsCollected);

                    myGame.RemoveChild(myGame.hud);
                    myGame.player.Reset();
                    StageLoader.ClearStage();
                    myGame.hud = new Hud();
                    myGame.AddChild(myGame.menu);
                    myGame.ShowMouse(true);

                    //myGame.menu.menuButtons[nextStage].Unlock();
                }
                
                else if (myGame.player.x > x && myGame.player.x < x + width)
                {
                    myGame.player.Reset();
                    myGame.player.SetBaseMovementSpeed(0);
                    myGame.player.SetXY(x,y);
                }
            }
        }

        /// <summary>
        /// Sets the stage that will be unlocked by the finish
        /// </summary>
        public void SetNextStage(Stages stage)
        {
            nextStage = stage;
        }
    }
}