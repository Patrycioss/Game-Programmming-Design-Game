using System;
using TiledMapParser;

namespace GXPEngine
{
    public class Menu : GameObject
    {
        private TiledLoader _tiledLoader;

        public Menu()
        {
            //Initializing
            _myGame.ShowMouse(true);
            
            
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
            Selection selection;

            switch (obj.Type)
            {
                case "Stage1":
                    selection = new Selection(Stages.stage1, true);
                    selection.SetXY(obj.X,obj.Y);
                    AddChild(selection);
                    break;
                
                case "Stage2":
                    selection = new Selection(Stages.stage2, true);
                    selection.SetXY(obj.X,obj.Y);
                    AddChild(selection);
                    break;
                
                case "Tutorial":
                    selection = new Selection(Stages.tutorial, false);
                    selection.SetXY(obj.X,obj.Y);
                    AddChild(selection);
                    break;
            }
        }
        
        
    }
}