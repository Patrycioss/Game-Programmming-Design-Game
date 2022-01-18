using System;

namespace GXPEngine
{
    public class StageCreator : GameObject
    {

        public StageCreator() {}
        
        public void SetStage(Enum stage)
        {
            switch (stage)
            {
                case Stages.Tutorial:
                    
                    //Load stage
                    _myGame.StageLoader.LoadStage("tutorial.tmx");

                    Finish finish = new Finish("sprites/tiles/checkers.png", 100, 700);
                    _myGame.StageLoader.stageContainer.AddChild(finish);
                    
                    break;
                
                
                case Stages.Stage1:

                    //Load stage
                    _myGame.StageLoader.LoadStage("stage1.tmx");
                    
                    //Add finish
                    finish = new Finish("sprites/tiles/checkers.png", 2100, 950);
                    _myGame.StageLoader.AddObject(finish);
                    
                    finish = new Finish("sprites/tiles/checkers.png", 100, 700);
                    _myGame.StageLoader.AddObject(finish);
                    
                    break;
                
                
                case Stages.Stage2:

                    //Load stage
                    _myGame.StageLoader.LoadStage("stage2.tmx");

                    break;
                

            }
        }
      
    }
}