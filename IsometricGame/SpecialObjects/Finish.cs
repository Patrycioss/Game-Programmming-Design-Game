using GXPEngine.Core;
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
        public Finish(string imagePath) : base(imagePath, addCollider: true)
        {
            collider.isTrigger = true;
        }

        void Update()
        {
            if (HitTest(myGame.player))
            {
                myGame.menu.menuButtons[StageLoader.currentStage.stage].SetCoinsCollected(myGame.hud.coinsCollected);
                
                myGame.RemoveChild(myGame.hud);
                myGame.player.Reset();
                StageLoader.ClearStage();
                myGame.hud = new Hud();
                myGame.AddChild(myGame.menu);

                myGame.menu.menuButtons[nextStage].Unlock();

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