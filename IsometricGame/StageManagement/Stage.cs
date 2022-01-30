using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GXPEngine.Core;
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
            // layers = new Pivot[2];
            //Layer[0]: background (tutorial information, walls)
            //Layer[1]: foreground (player,enemies)

            // for (int i = 0; i < layers.Length; i++)
            // {
            //     layers[i] = new Pivot();
            // }
            
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

            Layer mainLayer = stageData.Layers[1];
            Layer background = stageData.Layers[0];

            //Layer initialization
            short[,] tileNumbers;

            //Initialize the background
            tileNumbers = background.GetTileArray();
            for (int col = 0; col < background.Width; col++)
            for (int row = 0; row < background.Height; row++)
            {
                int x = col * tileSize;
                int y = row * tileSize;

                switch (tileNumbers[col, row])
                {
                    //Background
                    case 10:
                        Sprite brownBackground = new Sprite("sprites/tiles/crates/brownBackground.png");
                        brownBackground.SetXY(x, y);
                        spriteBatch.AddChild(brownBackground);
                        break;
                    case 11:
                        Sprite redBackground = new Sprite("sprites/tiles/crates/redBackground.png");
                        redBackground.SetXY(x, y);
                        spriteBatch.AddChild(redBackground);
                        break;
                    case 12:
                        Sprite blueBackground = new Sprite("sprites/tiles/crates/blueBackground.png");
                        blueBackground.SetXY(x, y);
                        spriteBatch.AddChild(blueBackground);
                        break;
                    case 13:
                        Sprite greenBackground = new Sprite("sprites/tiles/crates/greenBackground.png");
                        greenBackground.SetXY(x, y);
                        spriteBatch.AddChild(greenBackground);
                        break;

                    //Misc
                    case 37:
                        Sprite leftArrow = new Sprite("sprites/misc/arrow_left.png");
                        leftArrow.SetXY(x, y);
                        spriteBatch.AddChild(leftArrow);
                        break;
                    case 28:
                        Sprite a = new Sprite("sprites/misc/A.png");
                        a.SetXY(x, y);
                        spriteBatch.AddChild(a);
                        break;
                    case 29:
                        Sprite d = new Sprite("sprites/misc/D.png");
                        d.SetXY(x, y);
                        spriteBatch.AddChild(d);
                        break;
                    case 30:
                        Sprite rightArrow = new Sprite("sprites/misc/arrow_right.png");
                        rightArrow.SetXY(x, y);
                        spriteBatch.AddChild(rightArrow);
                        break;
                    case 31:
                        Sprite upArrow = new Sprite("sprites/misc/arrow_up.png");
                        upArrow.SetXY(x, y);
                        spriteBatch.AddChild(upArrow);
                        break;
                    case 39:
                        Sprite space = new Sprite("sprites/misc/space.png");
                        space.SetXY(x, y);
                        spriteBatch.AddChild(space);
                        break;
                }
            }
            //Add the background            
            spriteBatch.Freeze();
            AddChild(spriteBatch);


            //Initialize the mainLayer
            tileNumbers = mainLayer.GetTileArray();
            for (int col = 0; col < mainLayer.Width; col++)
            for (int row = 0; row < mainLayer.Height; row++)
            {
                int x = col * tileSize;
                int y = row * tileSize;
                switch (tileNumbers[col, row])
                {
                    //Crates
                    case 1:
                        Sprite brown = new Sprite("sprites/tiles/crates/brown.png");
                        brown.SetXY(x, y);
                        AddChild(brown);
                        break;
                    case 2:
                        Sprite red = new Sprite("sprites/tiles/crates/red.png");
                        red.SetXY(x, y);
                        AddChild(red);
                        break;
                    case 3:
                        Sprite blue = new Sprite("sprites/tiles/crates/blue.png");
                        blue.SetXY(x, y);
                        AddChild(blue);
                        break;
                    case 4:
                        Sprite green = new Sprite("sprites/tiles/crates/green.png");
                        green.SetXY(x, y);
                        AddChild(green);
                        break;
                    case 19:
                        Sprite brownGrey = new Sprite("sprites/tiles/crates/brownGrey.png");
                        brownGrey.SetXY(x, y);
                        AddChild(brownGrey);
                        break;
                    case 20:
                        Sprite redGrey = new Sprite("sprites/tiles/crates/redGrey.png");
                        redGrey.SetXY(x, y);
                        AddChild(redGrey);
                        break;
                    case 21:
                        Sprite blueGrey = new Sprite("sprites/tiles/crates/blueGrey.png");
                        blueGrey.SetXY(x, y);
                        AddChild(blueGrey);
                        break;
                    case 22:
                        Sprite greenGrey = new Sprite("sprites/tiles/crates/greenGrey.png");
                        greenGrey.SetXY(x, y);
                        AddChild(greenGrey);
                        break;

                    //Bricks
                    case 6:
                        Sprite bBrown = new Sprite("sprites/tiles/bricks/brown.png");
                        bBrown.SetXY(x, y);
                        AddChild(bBrown);
                        break;
                    case 15:
                        Sprite bGrey = new Sprite("sprites/tiles/bricks/grey.png");
                        bGrey.SetXY(x, y);
                        AddChild(bGrey);
                        break;
                    case 23:
                        Sprite bRed = new Sprite("sprites/tiles/bricks/red.png");
                        bRed.SetXY(x, y);
                        AddChild(bRed);
                        break;

                    //Ground
                    case 33:
                        Sprite dirt = new Sprite("sprites/tiles/dirt.png");
                        dirt.SetXY(x, y);
                        AddChild(dirt);
                        break;
                    case 24:
                        Sprite grass = new Sprite("sprites/tiles/grass.png");
                        grass.SetXY(x, y);
                        AddChild(grass);
                        break;

                    //Random
                    case 5:
                        Sprite black = new Sprite("sprites/tiles/black.png");
                        black.SetXY(x, y);
                        AddChild(black);
                        break;
                    case 14:
                        Sprite spawn = new Sprite("sprites/tiles/spawn.png");
                        spawn.SetXY(x, y);
                        AddChild(spawn);
                        break;
                    case 42:
                        Finish finish = new Finish("sprites/tiles/finish.png");
                        finish.SetXY(x, y);
                        switch (stage)
                        {
                            case Stages.Stage1:
                                finish.SetNextStage(Stages.Stage2);
                                break;
                            case Stages.Stage2:
                                finish.SetNextStage(Stages.Stage3);
                                myGame.player.currentPowerup = new FireBulletShooter();
                                break;
                        }
                        AddChild(finish);
                        break;

                    //Collectibles
                    case 7:
                        Coin coin = new Coin();
                        coin.SetXY(x, y);
                        AddChild(coin);
                        break;
                    case 8:
                        FireBulletShooter fireBulletShooter = new FireBulletShooter();
                        fireBulletShooter.SetXY(x, y);
                        AddChild(fireBulletShooter);
                        break;
                    case 16:
                        Heart heart = new Heart();
                        heart.SetXY(x + 16, y + 16);
                        AddChild(heart);
                        break;
                }
            }

        
            //Setup barriers on the sides of the map
            Sprite barrier = new Sprite("sprites/tiles/checkers.png");
            barrier.SetXY(-20, 0);
            barrier.width = 20;
            barrier.height = stageHeight;
            AddChild(barrier);

            barrier = new Sprite("sprites/tiles/checkers.png");
            barrier.SetXY(stageWidth, 0);
            barrier.width = 20;
            barrier.height = stageHeight;
            AddChild(barrier);

            //Create all Signs/Text
            if (stageData.ObjectGroups[1].Objects != null)
            {
                foreach (TiledObject obj in stageData.ObjectGroups[1].Objects)
                {
                    switch (obj.GID)
                    {
                        case 32:
                            Sign sign = new Sign(obj.Name);
                            sign.SetXY(obj.X, obj.Y - 64);
                            AddChild(sign);
                            break;

                        default:
                            if (obj.textField != null)
                            {
                                EasyDraw canvas = new EasyDraw((int) obj.Width * 5, (int) obj.Height * 5, false);
                                canvas.SetXY(obj.X, obj.Y);
                                canvas.TextAlign(CenterMode.Min, CenterMode.Min);
                                canvas.Text(obj.textField.text);
                                AddChild(canvas);
                                break;
                            } 
                            break;
                    }
                }
            }
            
            //Create all entities
            if (stageData.ObjectGroups[0].Objects != null)
            {
                foreach (TiledObject obj in stageData.ObjectGroups[0].Objects)
                {
                    float x = obj.X;
                    float y = obj.Y - 64;
                    

                    switch (obj.GID)
                    {
                        case 17:
                            Flyer flyer = new Flyer();
                            flyer.SetXY(x,y);
                            AddChild(flyer);
                            break;
                        case 25:
                            BlueGhost blueGhost = new BlueGhost();
                            blueGhost.SetXY(x,y);
                            AddChild(blueGhost);
                            break;
                        case 26:
                            RedGhost redGhost = new RedGhost();
                            redGhost.SetXY(x,y);
                            AddChild(redGhost);
                            break;
                        case 34:
                            GreenSlime slime = new GreenSlime();
                            slime.SetXY(x,y + 27);
                            AddChild(slime);
                            break;
                        case 35:
                            myGame.player = new Player();
                            myGame.player.SetXY(x,y);
                            AddChild(myGame.player);
                            break;
                    }
                }
            }
        }
    }
}