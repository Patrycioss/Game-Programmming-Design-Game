using System.Collections.Generic;
using GXPEngine.StageManagement;
using TiledMapParser;

namespace GXPEngine.UserInterface.Menu
{
    /// <summary>
    /// Class to store all information of the menu
    /// </summary>
    public class Menu : GameObject
    {
        public Dictionary<Stages, MenuButton> menuButtons = new Dictionary<Stages, MenuButton>();   

        public Menu()
        {
            //Initializing
            myGame.ShowMouse(true);

            
            menuButtons.Add(Stages.Stage1,new MenuButton(Stages.Stage1, false, 10));
            menuButtons.Add(Stages.Stage2,new MenuButton(Stages.Stage2, true, 10));
            menuButtons.Add(Stages.Stage3,new MenuButton(Stages.Stage3, true, 10));



            foreach (MenuButton selection in menuButtons.Values)
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
                    menuButtons[Stages.Stage1].SetXY(obj.X,obj.Y);
                    break;
                
                case "Stage2":
                    menuButtons[Stages.Stage2].SetXY(obj.X,obj.Y);
                    break;
                
                case "Stage3":
                    menuButtons[Stages.Stage3].SetXY(obj.X,obj.Y);
                    break;
            }
        }
    }
}