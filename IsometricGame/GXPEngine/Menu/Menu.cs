using System;
using System.Collections.Generic;
using TiledMapParser;

namespace GXPEngine
{
    public class Menu : GameObject
    {
        private TiledLoader _tiledLoader;

        public Dictionary<Stages, Selection> Selections;   

        public Menu()
        {
            //Initializing
            _myGame.ShowMouse(true);

            Selections = new Dictionary<Stages, Selection>();
            
            Selections.Add(Stages.Tutorial,new Selection(Stages.Tutorial, false, 9));
            Selections.Add(Stages.Stage1,new Selection(Stages.Stage1, true, 0));
            Selections.Add(Stages.Stage2,new Selection(Stages.Stage2, true, 0));


            foreach (Selection selection in Selections.Values)
            {
                AddChild(selection);
            }
            
            
            
            _tiledLoader = new TiledLoader("tiled/menu.tmx", this);
            _tiledLoader.LoadTileLayers();

            
            
            //ManualTypes
            _tiledLoader.AddManualType("Stage1");
            _tiledLoader.AddManualType("Stage2");
            _tiledLoader.AddManualType("Stage3");

            
            //Loading
            


            _tiledLoader.OnObjectCreated += OnWeirdObjectCreated;

            _tiledLoader.LoadObjectGroups();
            _tiledLoader.OnObjectCreated -= OnWeirdObjectCreated;
        }

        void Update()
        {
            
        }

        void OnWeirdObjectCreated(Sprite sprite, TiledObject obj)
        {

            switch (obj.Type)
            {
                
                
                case "Stage1":
                    Selections[Stages.Stage1].SetXY(obj.X,obj.Y);
                    

                    break;
                
                case "Stage2":
                    Selections[Stages.Stage2].SetXY(obj.X,obj.Y);
        
                    break;
                
                case "Tutorial":
                    Selections[Stages.Tutorial].SetXY(obj.X,obj.Y);
     
                    break;

            }
        }
        
        
    }
}