using System;
using GXPEngine.Core;
using GXPEngine.Managers;
using TiledMapParser;

namespace GXPEngine
{
    public class Hud : GameObject
    {
        private TiledLoader _tiledLoader;
        private EasyDraw canvas;
        
        public int coinAmount;
        
        private Vector2 coinAmountPos;
        private Vector2 healthBarOrigin;

        public Sprite[] hearts;
        

        public Hud()
        {
            //Initializing variables
            canvas = new EasyDraw(_myGame.width, _myGame.height, false);
            _tiledLoader = new TiledLoader("tiled/hud.tmx", canvas, false);

            //Monetary Gains
            coinAmount = 0;
            coinAmountPos = new Vector2(1000,1000);
            
            //HP
            hearts = new Sprite[3];
            healthBarOrigin = new Vector2(-1000, -1000);
            
            //Place to add extra exceptions
            _tiledLoader.AddManualType("CoinAmount");
            _tiledLoader.AddManualType("HealthBarOrigin");
            
            //Loading objects
            _tiledLoader.OnObjectCreated += OnWeirdObjectCreated;
            _tiledLoader.LoadObjectGroups();
            _tiledLoader.OnObjectCreated -= OnWeirdObjectCreated;
      

            //Canvas properties
            canvas.TextSize(70);
            canvas.Fill(0);
            canvas.TextAlign(CenterMode.Min,CenterMode.Min);
            
            //Adds canvas to display hierarchy
            AddChild(canvas);
            
            //Starting text
            UpdateCanvas();
        }
        
       public void UpdateCanvas()
        {
            canvas.ClearTransparent();
            canvas.Text(coinAmount.ToString(),coinAmountPos.x,coinAmountPos.y);
        }

       void OnWeirdObjectCreated(Sprite sprite, TiledObject obj)
        {
            
            switch (obj.Type)
            {
                case "CoinAmount":
                    coinAmountPos.Set(obj.X,obj.Y - 32);
                    break;
                
                case "HealthBarOrigin":
                    healthBarOrigin.Set(obj.X,obj.Y);

                    for (int i = 0; i < _myGame.player.health; i++)
                    {
                        AddHeart();
                    }
                    
                    break;
            }
            
        }

        public void AddCoinAmount(int amount)
        {
            coinAmount += amount;
            UpdateCanvas();
        }

        public void RemoveHeart()
        {
            for (int i = hearts.Length - 1; i >= 0; i--)
            {
                if (hearts[i] != null)
                {
                    hearts[i].LateDestroy();
                    hearts[i] = null;
                    break;
                }
            }
            UpdateCanvas();
        }

        public void AddHeart()
        {
            for (int i = 0; i < hearts.Length; i++)
            {
                if (hearts[i] == null)
                {
                    hearts[i] = new Sprite("sprites/collectibles/heart/heart_big.png");
                    hearts[i].SetXY(healthBarOrigin.x - (64 * i), healthBarOrigin.y);
                    canvas.AddChild(hearts[i]);

                    break;
                }
            }
            
            UpdateCanvas();
        }

        public override void AddHealth(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                AddHeart();
            }
        }
    }
}