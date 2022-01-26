using System.Collections.Generic;
using GXPEngine.StageManagement;
using TiledMapParser;

namespace GXPEngine.UserInterface.Menu
{
    public class Menu : GameObject
    {
        private readonly Dictionary<Stages, Selection> selections = new Dictionary<Stages, Selection>();   

        public Menu()
        {
            //Initializing
            myGame.ShowMouse(true);

            selections.Add(Stages.Tutorial,new Selection(Stages.Tutorial, false, 9));
            selections.Add(Stages.Stage1,new Selection(Stages.Stage1, true, 0));
            selections.Add(Stages.Stage2,new Selection(Stages.Stage2, true, 0));


            foreach (Selection selection in selections.Values)
            {
                AddChild(selection);
            }
            
            TiledLoader tiledLoader = new TiledLoader("tiled/menu.tmx", this);
            tiledLoader.LoadTileLayers();

            
            
            //ManualTypes
            tiledLoader.AddManualType("Stage1");
            tiledLoader.AddManualType("Stage2");
            tiledLoader.AddManualType("Stage3");

            
            //Loading
            


            tiledLoader.OnObjectCreated += OnWeirdObjectCreated;

            tiledLoader.LoadObjectGroups();
            tiledLoader.OnObjectCreated -= OnWeirdObjectCreated;
        }
        
        void OnWeirdObjectCreated(Sprite sprite, TiledObject obj)
        {

            switch (obj.Type)
            {
                
                
                case "Stage1":
                    selections[Stages.Stage1].SetXY(obj.X,obj.Y);
                    

                    break;
                
                case "Stage2":
                    selections[Stages.Stage2].SetXY(obj.X,obj.Y);
        
                    break;
                
                case "Tutorial":
                    selections[Stages.Tutorial].SetXY(obj.X,obj.Y);
     
                    break;

            }
        }
        
        
    }
}