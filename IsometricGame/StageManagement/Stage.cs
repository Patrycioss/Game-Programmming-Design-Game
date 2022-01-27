using System;
using System.Collections.Generic;
using GXPEngine.Entities;
using GXPEngine.Entities.Enemies;
using GXPEngine.SpecialObjects;
using GXPEngine.SpecialObjects.Collectibles;
using TiledMapParser;

namespace GXPEngine.StageManagement
{
    /// <summary>
    /// The stage class contains all information about a stage
    /// </summary>
    public class Stage : GameObject
    {

        private readonly Map stageData;
        private readonly int tileSize;
        private readonly Pivot[] layers;
        
        public int stageHeight;
        public int stageWidth;

        public readonly Stages stage;
        
        //Use this to group all tiles without collision together
        private SpriteBatch spriteBatch;

        public Stage(Stages stage)
        {
            //All the layers, add tiled manually to the layers
            layers = new Pivot[2];
            //Layer[0]: background (tutorial information, walls)
            //Layer[1]: foreground (player,enemies)

            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Pivot();
            }
            
            myGame.AddChild(this);

            this.stage = stage;
            string stagePath = "tiled/stages/" + stage.ToString() + ".tmx";
            stageData = MapParser.ReadMap(stagePath);
            tileSize = stageData.TileWidth;
            
            if (stageData.Layers == null || stageData.Layers.Length <= 0)
            {
                throw new Exception("Tile file " + stagePath + " does not contain a layer!");
            }
            
            LoadStage();
        }
        
        
        
        private void LoadStage()
        {
            stageWidth = stageData.Width * stageData.TileWidth;
            stageHeight = stageData.Height * stageData.TileHeight;

            spriteBatch = new SpriteBatch();

            Layer mainLayer = stageData.Layers[0];
            
            short[,] tileNumbers = mainLayer.GetTileArray();

            for (int col = 0; col < mainLayer.Width; col++)
            for (int row = 0; row < mainLayer.Height; row++) 
            {
                switch (tileNumbers[col, row])
                {
                //Entities
                    case 210:
                        myGame.player = new Player();
                        myGame.player.SetXY(col*tileSize, row*tileSize);
                        layers[1].AddChild(myGame.player);
                        break;
                    
                    case 79:
                        myGame.player = new Player();
                        myGame.player.SetXY(col*tileSize, row*tileSize);
                        layers[1].AddChild(myGame.player);
                        break;
                    
                    case 122:
                        GreenSlime slime = new GreenSlime();
                        slime.SetXY(col*tileSize,row*tileSize + 27);
                        layers[1].AddChild(slime);
                        break;
                    
                    case 116:
                        Flyer flyer = new Flyer();
                        flyer.SetXY(col*tileSize,row*tileSize);
                        layers[1].AddChild(flyer);
                        break;
                    
                    case 128:
                        RedGhost redGhost = new RedGhost();
                        redGhost.SetXY(col*tileSize,row*tileSize);
                        layers[1].AddChild(redGhost);
                        break;
                    
                    case 130:
                        BlueGhost blueGhost = new BlueGhost();
                        blueGhost.SetXY(col*tileSize,row*tileSize);
                        layers[1].AddChild(blueGhost);
                        break;
                    
                //Collectibles
                    case 123:
                        Coin coin = new Coin();
                        coin.SetXY(col * tileSize, row * tileSize);
                        AddChild(coin);
                        break;
                    
                    case 124:
                        Heart heart = new Heart();
                        heart.SetXY(col*tileSize + 16, row * tileSize + 16);
                        AddChild(heart);
                        break;
                    
                    case 125:
                        FireBulletShooter fireBulletShooter = new FireBulletShooter();
                        fireBulletShooter.SetXY(col*tileSize, row * tileSize);
                        AddChild(fireBulletShooter);
                        break;
                    
                //Tiles
                    case 89:
                        Sprite grass = new Sprite("sprites/tiles/grass.png");
                        grass.SetXY(col*tileSize,row*tileSize);
                        AddChild(grass);
                        break;
                    
                    case 12:
                        Sprite blackTile = new Sprite("sprites/tiles/blackTile.png");
                        blackTile.SetXY(col*tileSize, row*tileSize);
                        AddChild(blackTile);
                        break;
                    
                    case 64:
                        Sprite spawn = new Sprite("sprites/tiles/spawn.png");
                        spawn.SetXY(col*tileSize,row*tileSize);
                        AddChild(spawn);
                        break;

                    case 2:
                        Sprite crate = new Sprite("sprites/tiles/crate.png");
                        crate.SetXY(col*tileSize,row*tileSize);
                        AddChild(crate);
                        break;
                    
                    case 3:
                        Sprite metal = new Sprite("sprites/tiles/metal.png");
                        metal.SetXY(col*tileSize,row*tileSize);
                        AddChild(metal);
                        break;
                    
                    case 4:
                        Sprite blue = new Sprite("sprites/tiles/blue.png");
                        blue.SetXY(col*tileSize,row*tileSize);
                        AddChild(blue);
                        break;
                    
                    case 30:
                        Sprite blueGrey = new Sprite("sprites/tiles/blue_grey.png");
                        blueGrey.SetXY(col*tileSize,row*tileSize);
                        AddChild(blueGrey);
                        break;

                case 74:
                    Finish finish = new Finish("sprites/tiles/finish.png");
                    finish.SetXY(col * tileSize, row * tileSize);
                    switch (stage)
                    {
                        case Stages.Tutorial:
                            finish.SetNextStage(Stages.Stage1);
                            break;
                        case Stages.Stage1:
                            finish.SetNextStage(Stages.Stage2);
                            break;
                    }
                        
                        
                        AddObjectAtLayer(finish,1);
                        break;
                    
                    
                //Tiles without collision
                     
                    case 147:
                        Sprite metalBackground = new Sprite("sprites/tiles/metal_walkthrough.png");
                        metalBackground.SetXY(col*tileSize,row*tileSize);
                        spriteBatch.AddChildAt(metalBackground, 0);
                        break;
                
                    case 91:
                        Sprite ground = new Sprite("sprites/tiles/ground.png");
                        ground.SetXY(col*tileSize, row*tileSize);
                        spriteBatch.AddChild(ground);
                        break;
                        
                        //Misc
                    case 110:
                        Sprite leftArrow = new Sprite("sprites/misc/arrow_left.png");
                        leftArrow.SetXY(col*tileSize, row*tileSize);
                        spriteBatch.AddChild(leftArrow);
                        break;
                    
                    case 105:
                        Sprite a = new Sprite("sprites/misc/A.png");
                        a.SetXY(col*tileSize,row*tileSize);
                        spriteBatch.AddChild(a);
                        break;
                    
                    case 106:
                        Sprite d = new Sprite("sprites/misc/D.png");
                        d.SetXY(col*tileSize,row*tileSize);
                        spriteBatch.AddChild(d);
                        break;
                    
                    case 107:
                        Sprite rightArrow = new Sprite("sprites/misc/arrow_right.png");
                        rightArrow.SetXY(col*tileSize, row*tileSize);
                        spriteBatch.AddChild(rightArrow);
                        break;
                    
                    case 112:
                        Sprite space = new Sprite("sprites/misc/space.png");
                        space.SetXY(col*tileSize, row*tileSize);
                        spriteBatch.AddChild(space);
                        break;
                    
                    case 108:
                        Sprite upArrow = new Sprite("sprites/misc/arrow_up.png");
                        upArrow.SetXY(col*tileSize,row*tileSize);
                        spriteBatch.AddChild(upArrow);
                        break;
                    

                }
            }
            spriteBatch.Freeze();
            layers[0].AddChild(spriteBatch);
            
            //Setup barriers
            Sprite barrier = new Sprite("sprites/tiles/checkers.png");
            barrier.SetXY(-20,0);
            barrier.width = 20;
            barrier.height = stageHeight;
            AddObjectAtLayer(barrier,1);

            barrier = new Sprite("sprites/tiles/checkers.png");
            barrier.SetXY(stageWidth,0);
            barrier.width = 20;
            barrier.height = stageHeight;
            AddObjectAtLayer(barrier,1);
            
            
            //Creates signs or
            //Creates text based on text placed in the tiledmap
            if (stageData.ObjectGroups[0].Objects != null)
            {
                foreach (TiledObject obj in stageData.ObjectGroups[0].Objects)
                {
                    switch (obj.ID)
                    {
                        case 257:
                            Sign sign = new Sign(obj.Name);
                            sign.SetXY(obj.X, obj.Y - 64);
                            layers[0].AddChild(sign);
                            break;

                        default:
                            EasyDraw canvas = new EasyDraw((int) obj.Width * 5, (int) obj.Height * 5, false);
                            canvas.SetXY(obj.X, obj.Y);
                            canvas.TextAlign(CenterMode.Min, CenterMode.Min);
                            canvas.Text(obj.textField.text);
                            layers[0].AddChild(canvas);
                            break;
                    }
                }
            }

            foreach (Pivot pivot in layers)
            {
                AddChild(pivot);
            }
        }

        /// <summary>
        /// Adds an object at the specified layer.
        /// [0]: background; [1]: foreground;
        /// </summary>
        public void AddObjectAtLayer(GameObject gameObject, int layer)
        {
            layers[layer].AddChild(gameObject);
        }

        /// <summary>
        /// Gets a list of objects from the stage
        /// </summary>
        public List<GameObject> GetObjects(bool includeBackground = false, bool includeForeGround = true)
        {
            List<GameObject> list = new List<GameObject>();

            if (includeBackground)
            {
                list.AddRange(layers[0].GetChildren());
            }
            if (includeForeGround)
            {
                list.AddRange(layers[1].GetChildren());
            }
            return list;
        }
    }
}