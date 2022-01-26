using GXPEngine.Core;
using TiledMapParser;

namespace GXPEngine.UserInterface
{
    public class Hud : GameObject
    {
        private readonly EasyDraw canvas;
        
        private int coinAmount;
        
        private Vector2 coinAmountPos;
        private Vector2 healthBarOrigin;

        private Sprite[] hearts;
        

        public Hud()
        {
            //Initializing variables
            canvas = new EasyDraw(myGame.width, myGame.height, false);
            TiledLoader tiledLoader = new TiledLoader("tiled/hud.tmx", canvas, false);

            //Monetary Gains
            coinAmount = 0;
            coinAmountPos = new Vector2(1000,1000);
            
            //HP
            hearts = new Sprite[myGame.player.maxHealth];
            healthBarOrigin = new Vector2(-1000, -1000);
            
            //Place to add extra exceptions
            tiledLoader.AddManualType("CoinAmount");
            tiledLoader.AddManualType("HealthBarOrigin");
            
            //Loading objects
            tiledLoader.OnObjectCreated += OnWeirdObjectCreated;
            tiledLoader.LoadObjectGroups();
            tiledLoader.OnObjectCreated -= OnWeirdObjectCreated;
      

            //Canvas properties
            canvas.TextSize(70);
            canvas.Fill(0);
            canvas.TextAlign(CenterMode.Min,CenterMode.Min);
            
            //Adds canvas to display hierarchy
            AddChild(canvas);
            
            //Starting text
            UpdateCanvas();
        }

        public void Reset()
        {
            coinAmount = 0;
            hearts = new Sprite[myGame.player.maxHealth];

            UpdateCanvas();
        }
        
       

        private void OnWeirdObjectCreated(Sprite sprite, TiledObject obj)
        {
            
            switch (obj.Type)
            {
                case "CoinAmount":
                    coinAmountPos.Set(obj.X,obj.Y - 32);
                    break;
                
                case "HealthBarOrigin":
                    healthBarOrigin.Set(obj.X,obj.Y);

                    for (int i = 0; i < hearts.Length; i++)
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
        
        public override void AddHealth(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                AddHeart();
            }
        }


        public void FixHearts()
        {
            //Resetting health in HUD
            if (myGame.player.health != hearts.Length)
            {
                if (myGame.player.health < hearts.Length)
                {
                    for (int i = 0; i < Mathf.Abs(myGame.player.health - hearts.Length); i++)
                    {
                        myGame.hud.RemoveHeart();
                    }
                }
                else
                {
                    for (int i = 0; i < Mathf.Abs(myGame.player.health - hearts.Length); i++)
                    {
                        myGame.hud.AddHeart();
                    }
                }
            }    
        }
        
        private void UpdateCanvas()
        {
            canvas.ClearTransparent();
            canvas.Text(coinAmount.ToString(),coinAmountPos.x,coinAmountPos.y);
        }
        
        private void AddHeart()
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
        
    }
}