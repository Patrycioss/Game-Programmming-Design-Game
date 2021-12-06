using GXPEngine.Objects;
using GXPEngine.Core;
using GXPEngine;

namespace GXPEngine.GXPEngine.SceneStuff
{
    public class MakeScene
    {
        private int width;
        private int height;
        

        public MakeScene(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        
        public Scene Template()
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
            scene.layers[0].AddObject(square);

            return scene;
        }
        
        public Scene Start()
        {
            
            //Create a new Scene with an amount of layers based on the depth of the scene
            Scene scene = new Scene(5);
            
            //Give the scene a name
            scene.name = "Start";
            
            //Create a gameObject based on a class and add it to the correct layer
            Square square = new Square();
            square.SetXY(0, 0);
            square.SetScaleXY(500,500);
            scene.layers[0].AddObject(square);

            return scene;
        }
        
    }
}