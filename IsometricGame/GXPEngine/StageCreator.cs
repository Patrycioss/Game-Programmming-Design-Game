using System;

namespace GXPEngine
{
    public class StageCreator : GameObject
    {
        private Finish finish;

        public StageCreator() {}
        
        public void SetStage(Enum stage)
        {
            switch (stage)
            {
                case Stages.tutorial:
                    
                    //Load stage
                    _myGame.StageLoader.LoadStage("tutorial.tmx");

                    break;
                
                
                case Stages.stage1:

                    //Load stage
                    _myGame.StageLoader.LoadStage("stage1.tmx");
                    
                    //Add finish
                    finish = new Finish("sprites/tiles/checkers.png", 2100, 950, Stages.stage2);
                    _myGame.StageLoader.AddObject(finish);
                    
                    finish = new Finish("sprites/tiles/checkers.png", 100, 700, Stages.stage2);
                    _myGame.StageLoader.AddObject(finish);
                    
                    break;
                
                
                case Stages.stage2:

                    //Load stage
                    _myGame.StageLoader.LoadStage("stage2.tmx");

                    break;
                

            }
        }
      
    }
}