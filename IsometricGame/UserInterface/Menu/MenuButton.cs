using GXPEngine.SpecialObjects.Collectibles;
using GXPEngine.StageManagement;

namespace GXPEngine.UserInterface.Menu
{
    /// <summary>
    /// Button object for the main menu
    /// </summary>
    public sealed class MenuButton : AnimationSprite
    {
        private readonly Stages targetStage;
        private readonly EasyDraw canvas; 

        private bool locked;

        private int coinsCollected;
        
        //Right now the amount of collectible coins has to be filled in manually as the stages are loaded after the menu
        private int collectibleCoins;

        public MenuButton(Stages stage, bool locked, int collectibleCoins) : base("sprites/selection/selection.png",3,1,3)
        {
            targetStage = stage;
            this.locked = locked;
            this.collectibleCoins = collectibleCoins;
            
            coinsCollected = 0;
            canvas = new EasyDraw(width, height);

            //Text for amount of collected coins
            canvas.Fill(0); 
            canvas.TextAlign(CenterMode.Center,CenterMode.Center);
            UpdateCanvas();
        
            AddChild(canvas);
        }


        void Update()
        {
            if (!locked)
            {
                if (collider.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    currentFrame = 1;

                    if (Input.GetMouseButton(0))
                    {
                        Press();
                    }
                }
                else currentFrame = 0;
            }
            else currentFrame = 2;
        }

        /// <summary>
        /// Changes the amoount of coins collected
        /// </summary>
        public void SetCoinsCollected(int amount)
        {
            coinsCollected = amount;
            UpdateCanvas();
        }

        /// <summary>
        /// Unlock this button
        /// </summary>
        public void Unlock()
        {
            locked = false;
        }

        /// <summary>
        /// Clears the canvas and updates it
        /// </summary>
        private void UpdateCanvas()
        {
            canvas.ClearTransparent();
            canvas.Text(coinsCollected + "/" + collectibleCoins, width*0.8f, height/2.0f);
        }
        

        /// <summary>
        /// This is what happens when the button is pressed
        /// </summary>
        private void Press()
        {
            myGame.RemoveChild(myGame.menu);
            StageLoader.LoadStage(targetStage);
            myGame.hud = new Hud();
            myGame.AddChild(myGame.hud);
            StageLoader.currentStage.x = 0;
            myGame.ShowMouse(false);

            if (targetStage == Stages.Stage3)
            {
                myGame.player.currentPowerup = new FireBulletShooter();
            }

        }

    }
}