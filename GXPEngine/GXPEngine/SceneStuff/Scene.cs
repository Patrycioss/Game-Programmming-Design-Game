using System.Collections;
using System.Collections.Generic;
using GXPEngine.SceneStuff;


namespace GXPEngine
{
    public class Scene
    {
        public List<Layer> layers;
        public string name;

        public Scene(int layerCount)
        {
            layers = new List<Layer>(layerCount);

            for (var i = 0; i < layerCount; i++) layers.Add(new Layer());
        }

        public void Update()
        {
            foreach (Layer layer in layers)
            {
                layer.Update();
            }
        }
    }
}