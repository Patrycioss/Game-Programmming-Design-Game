using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using GXPEngine.Core;
using GXPEngine.Enemies;
using TiledMapParser;

namespace GXPEngine
{
    public class StageLoader : GameObject
    {
        public Pivot stageContainer;
        public TiledLoader currentStage;
        public string currentPath;

        private Map info;
        private string _folderName;
        private Assembly _callingAssembly;

        private string invisType;
        

        public StageLoader()
        {
            stageContainer = new Pivot();
            currentStage = null;
            currentPath = null;
        }

        public void Clear()
        {
            foreach (GameObject gameObject in stageContainer.GetChildren())
            {
                gameObject.Remove();
            }

            currentPath = null;
            currentStage = null;
        }
        
        public void LoadStage(string stagePath)
        {
            stageContainer.SetXY(0,0);
            
            stagePath = ("tiled/stages/" + stagePath);
            
            
            _folderName=Path.GetDirectoryName(stagePath);
            _callingAssembly = Assembly.GetCallingAssembly();

            

            if (stagePath != currentPath)
            {
            
                
                if (currentStage != null)
                {
                    foreach (GameObject gameObject in stageContainer.GetChildren())
                    {
                        gameObject.Remove();
                    }
                }
                
                currentStage = new TiledLoader(stagePath, stageContainer);
                currentPath = stagePath;
                

                currentStage.rootObject = stageContainer;

                info = MapParser.ReadMap(stagePath);
                
                
                //Blocks
                currentStage.AddManualType("WalkthroughMetal");
                
                //Entities    
                currentStage.AddManualType("Player");
                
                currentStage.AddManualType("Slime");
                currentStage.AddManualType("Flyer");
                currentStage.AddManualType("Ghost");
                
                //Collectibles    
                currentStage.AddManualType("Coin");
                currentStage.AddManualType("Heart");
                
                currentStage.AddManualType("Fire");
                
                //Signs
                currentStage.AddManualType("Sign");

                //Loading    
                currentStage.OnObjectCreated += OnWeirdObjectCreated;
                currentStage.LoadTileLayers();
                currentStage.LoadObjectGroups();
                currentStage.OnObjectCreated -= OnWeirdObjectCreated;


            }
        }

        private void OnWeirdObjectCreated(Sprite sprite, TiledObject obj)
        {
            Console.WriteLine(obj.Type);


            switch (obj.Type)
            {
                
                case "WalkthroughMetal":
                
                    sprite = new Sprite("sprites/tiles/WalkthroughMetal.png"); 
                    sprite.collider.isTrigger = true;
                    
                    sprite.SetXY(obj.X, obj.Y - 64);
                    stageContainer.AddChild(sprite);

                    break;
                
                case "Player":
                    
                    _myGame.player.SetXY(obj.X,obj.Y - 64);
                    stageContainer.AddChild(_myGame.player);
                    
                    break;
                
                case "Coin":

                    Coin coin = new Coin();
                    coin.SetXY(obj.X,obj.Y - 64);
                    stageContainer.AddChild(coin);

                    break;
                
                case "Heart":
                    Heart heart = new Heart();
                    heart.SetXY(obj.X + 16,obj.Y - 48);
                    stageContainer.AddChild(heart);
                
                    break;
                
                case "Slime":
                    
                    Basic slime = new GreenSlime();
                    Console.WriteLine("hoi");

                    if (obj.MirrorY)
                    {
                        Console.WriteLine("haha");
                        slime.Mirror(true,false);
                        slime.SetXY(obj.X-64,obj.Y);
                    }
                    else
                    {
                        slime.SetXY(obj.X,obj.Y - 37);
                    }

                    
                    stageContainer.AddChild(slime);
                    break;
                
                case "Flyer":

                    Basic flyer = new Flyer();
                    
                    if (obj.MirrorY)
                    {
                        Console.WriteLine("haha");
                        flyer.Mirror(true,false);
                        flyer.SetXY(obj.X-64,obj.Y);
                    }
                    else
                    {
                        flyer.SetXY(obj.X,obj.Y - 64);
                    }
                    
                    stageContainer.AddChild(flyer);
                    break;
                
                case "Ghost":

                    Ghost ghost;

                    if (obj.HasProperty("red", "string"))
                    {
                        ghost = new RedGhost();
                    }
                    else if (obj.HasProperty("blue", "string"))
                    {
                        ghost = new BlueGhost();
                    }

                    else
                    {
                        throw new Exception("Ghost has no color, add color by adding a custom property in tiled with color name");
                    }

                    ghost.SetXY(obj.X,obj.Y-64);
                    stageContainer.AddChild(ghost);
                    break;
                
                
                
                case "Sign":
                    Sign sign = new Sign(obj.Name);
                    sign.SetXY(obj.X, obj.Y-64);
                    stageContainer.AddChildAt(sign,1);
                    break;
                
                case "Fire":
                    FireBulletShooter fireBulletShooter = new FireBulletShooter();
                    fireBulletShooter.SetXY(obj.X,obj.Y - 64);
                    stageContainer.AddChild(fireBulletShooter);
                    break;
                
                default:
                    invisType = obj.Type;
                    break;
                    
            }
            
        }

        public void AddObject(GameObject gameObject)
        {
            stageContainer.AddChild(gameObject);
        }

        public void AddObjectAt(GameObject gameObject, float x, float y)
        {
            gameObject.SetXY(x,y);
            stageContainer.AddChild(gameObject);
        }

        public void AddObjectAt(GameObject gameObject, Vector2 vector)
        {
            AddObjectAt(gameObject,vector.x,vector.y);
        }
    }
}