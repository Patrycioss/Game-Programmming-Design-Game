using System;

namespace GXPEngine
{
    public static class StageLoader
    {
        public static Stage currentStage;

        public static float x, y;
        
        public static void LoadStage(Stages stage)
        {
            if (currentStage != null)
            {
                currentStage.Destroy();
            }
            
            
            //Add file structure text
            string stageName = "tiled/stages/" + stage.ToString() + ".tmx";
            
            
            currentStage = new Stage(stageName);
            
            x = currentStage.x;
            y = currentStage.y;

        }

        public static void ClearStage()
        {
            if (currentStage != null)
            {
                currentStage.LateDestroy();
                
            }
            else throw new Exception("There isn't any stage at the moment to get rid of I'm afraid!");
        }

        public static void AddObjectAtLayer(GameObject gameObject, int layer)
        {
            currentStage.AddObjectAtLayer(gameObject,layer);
        }
        
    }
}