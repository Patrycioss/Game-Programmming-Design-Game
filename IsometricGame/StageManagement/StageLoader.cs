using System;

namespace GXPEngine.StageManagement
{
    public static class StageLoader
    {
        public static Stage currentStage;
        
        public static void LoadStage(Stages stage)
        {
            if (currentStage != null)
            {
                currentStage.Destroy();
            }
            
            
            //Add file structure text
            string stagePath = "tiled/stages/" + stage.ToString() + ".tmx";
            
            
            currentStage = new Stage(stagePath);
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