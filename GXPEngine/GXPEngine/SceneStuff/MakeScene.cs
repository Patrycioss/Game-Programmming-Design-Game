using System;
using GXPEngine.Objects;
using GXPEngine.Core;
using GXPEngine;

namespace GXPEngine.GXPEngine.SceneStuff
{
    public static class MakeScene
    {
        public static Scene Template()
        {
            
            //Create a new Scene with an amount of layers based on the depth of the scene
            Scene scene = new Scene(5);
            
            //Give the scene a name
            scene.name = "Start";
            
            //Create a gameObject based on a class and add it to the correct layer
            Square square = new Square();
            square.SetXY(0, 0);
            square.width = 100;
            square.height = 100;
            square.name = "s";
            scene.layers[0].AddObject(square);

            return scene;
        }
        
        public static Scene Start()
        {
            
            //Create a new Scene with an amount of layers based on the depth of the scene
            Scene scene = new Scene(5);
            
            //Give the scene a name
            scene.name = "Start";
            
            //Create a gameObject based on a class and add it to the correct layer
            Square square = new Square();
            square.SetXY(0, 0);
            square.width = 10;
            square.height = 10;

            square.name = "s1";
            Console.WriteLine(square.name);
            scene.layers[3].AddObject(square);

            ///
            Square otherSquare = new Square();
            otherSquare.SetXY(5,5);
       
            otherSquare.name = "s2";
            scene.layers[2].AddObject(otherSquare);
            return scene;
        }
        
    }
}