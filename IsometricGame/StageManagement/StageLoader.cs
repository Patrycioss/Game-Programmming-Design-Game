using System;
using System.Collections.Generic;

namespace GXPEngine.StageManagement
{
    /// <summary>
    /// This class has all the loading functionality of stages
    /// </summary>
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
          
            
            
            currentStage = new Stage(stage);
        }

          
        /// <summary>
        /// Get rid of the current stage
        /// </summary>
        public static void ClearStage()
        {
            if (currentStage != null)
            {
                currentStage.LateDestroy();
                
            }
            else throw new Exception("There isn't any stage at the moment to get rid of I'm afraid!");
        }


        public static List<GameObject> GetObjects()
        {
            return currentStage.GetChildren();
        }

        public static void AddObject(GameObject gameObject)
        {
            currentStage.AddChild(gameObject);
        }
        
    }
}