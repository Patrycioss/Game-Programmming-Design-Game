using System;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Selection : AnimationSprite
    {
        private Stages targetStage;
        private bool locked;

        public Selection(Stages stage, bool locked) : base("sprites/selection/selection.png",3,1,3)
        {
            targetStage = stage;
            this.locked = locked;
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

            _myGame.StageCreator.SetStage(targetStage);
            _myGame.RemoveChild(_myGame.menu);
            _myGame.AddChild(_myGame.StageLoader.stageContainer);
            _myGame.AddChild(_myGame.hud);
            
        }

    }
}