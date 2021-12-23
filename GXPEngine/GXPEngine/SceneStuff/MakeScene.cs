using System;
using GXPEngine.Objects;
using GXPEngine.Core;
using GXPEngine;

namespace GXPEngine
{
    public class MakeScene : GameObject
    {
        private MyGame _myGame;
        private int width, height;
        private int horizontalGap, verticalGap;

        public MakeScene()
        {
            _myGame = (MyGame) game;
            x = _myGame.width * 0.1f;
            y = _myGame.height * 0.5f;
            width = (int)(_myGame.width * 0.8f);
            height = (int) (_myGame.height * 0.5f);
            horizontalGap = width / 15;
            verticalGap = height / 5;
        }
        
        //TemplateScene
        // public Scene Template()
        // {
            //Create a new Scene with an amount of layers based on the depth of the scene
                // Scene scene = new Scene();
            
            //Give the scene a name
                // scene.name = "Name";
            
            //Create a gameObject
                //Object object = new Object();
                //object.name = "objectName";
                //object.SetXY(x,y);
                //object.width = w;
                //object.height = h;

            //Add gameObject to the scene (Use debug mode (PRESS B) to see what layer has what number)
                //scene.layers[layerNumber].AddObject(object);
                
            // return scene;
        // }
        
        public Scene Start()
        {
            
            //Create a new Scene with an amount of layers based on the depth of the scene
            Scene scene = new Scene();
            
            //Give the scene a name
            scene.name = "Start";
            
            //Create a gameObject based on a class and add it to the correct layer
            Square square = new Square();
            square.SetXY(horizontalGap * 5, verticalGap * 3);
            square.width = scene.horizontalGap;
            square.height = scene.verticalGap;

            square.name = "s1";
            Console.WriteLine(square.name);
            scene.layers[3].AddObject(square);

            ///
            Square otherSquare = new Square();
            otherSquare.SetXY(horizontalGap * 4, 0);
            otherSquare.name = "s2";
            otherSquare.width = horizontalGap;
            otherSquare.height = verticalGap * 2;
            
            
            scene.layers[1].AddObject(otherSquare);
            return scene;
        }
        
    }
}