using System;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Selection : AnimationSprite
    {
        private Stages targetStage;
        private bool locked;

        private EasyDraw canvas;
        public int coinsCollected;
        public int collectibleCoins;

        public Selection(Stages stage, bool locked, int collectibleCoins) : base("sprites/selection/selection.png",3,1,3)
        {
            targetStage = stage;
            this.locked = locked;
            this.collectibleCoins = collectibleCoins;
            canvas = new EasyDraw(width, height);
            
            
            //Text for amount of collected coins
            canvas.Fill(0); 
            canvas.TextAlign(CenterMode.Center,CenterMode.Center);
            canvas.Text(coinsCollected + "/" + collectibleCoins, width*0.8f, height/2);
            coinsCollected = 0;
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

        void Press()
        {
            _myGame.RemoveChild(_myGame.menu);
            StageLoader.LoadStage(targetStage);
            _myGame.hud = new Hud();
            _myGame.AddChild(_myGame.hud);
            
        }

    }
}