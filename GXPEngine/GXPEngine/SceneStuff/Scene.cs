using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using TiledMapParser;
using Layer = GXPEngine.SceneStuff.Layer;


namespace GXPEngine
{
    public class Scene : GameObject
    {
        private MyGame _myGame;
        
        public List<Layer> layers;
        public new string name;

        protected bool debugging = false;
        public int horizontalGap, verticalGap;

        public int width, height;

        protected EasyDraw sceneCanvas;
            
        public Scene()
        {
            //Used to get information from the main MyGame.cs file
            _myGame = (MyGame) game;

            //Set the position and size of the scene
            x = _myGame.width * 0.1f;
            y = _myGame.height * 0.5f;
            width = (int)(_myGame.width * 0.8);
            height = (_myGame.height/2);
            
            //Determine the size of the grid spaces based on the layercount and the dimensions of the scene
            horizontalGap = width / 15;
            verticalGap = height / 5;

            //Make a canvas and set the position to the position of the scene 
            sceneCanvas = new EasyDraw(width, height, false);
            sceneCanvas.HorizontalShapeAlign = CenterMode.Min;
            sceneCanvas.VerticalShapeAlign = CenterMode.Min;
            
            //Add the canvas to the scene (used for grid)
            AddChild(sceneCanvas);
            
            //Make a list of layers and fill it with layers equal to the given layerCount variable
            layers = new List<Layer>(5);
            for (var i = 0; i < layers.Capacity; i++)
            {
                layers.Add(new Layer());
            }
            
            //Add the layers to the scene
            for (int i = 0; i < layers.Capacity; i++)
            {
                AddChild(layers[i]);
            }
            
           

        }

        public void Update()
        {
            foreach (Layer layer in layers)
            {
                layer.Update();
            }
        }

        public void MoveFromLayerToOther(string objectName, int first, int now)
        {
            GameObject gameObject = layers[first].gameObjects[objectName];
           
            //Remove from old layer
            layers[first].gameObjects.Remove(gameObject.name);

            //Add to new layer
            layers[now].AddObject(gameObject);
        }

        public void ToggleDebug()
        {
            debugging = !debugging;
            Console.WriteLine("JAWEL HOOR");

            if (debugging)
            {
                sceneCanvas.Fill(255,0,0);
                sceneCanvas.StrokeWeight(5);
                for (int i = 0; i < height; i+= verticalGap)
                {
                    sceneCanvas.Line(0,i,width-5,i);
                    
                    sceneCanvas.Fill(255);
                    sceneCanvas.TextSize(40);
                    if (i == 0)
                    {
                        sceneCanvas.Text("0",0, i);
                    }
                    sceneCanvas.Text((i/verticalGap).ToString(), 0,  i + verticalGap);
                  
                }

                for (int i = 0; i < width; i += horizontalGap)
                {
                    sceneCanvas.Line(i,0,i,height);
                }
                
               
                
            }
            else sceneCanvas.Clear(0,0,0,0);

        }

        public bool ObjectIsInLayer(GameObject givenObject, int layer)
        {
            foreach (GameObject gameObject in layers[layer].gameObjects.Values)
            {
                if (gameObject == givenObject)
                {
                    return true;
                }
            }

            return false;
        }
    }
}