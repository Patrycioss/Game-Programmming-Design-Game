using System;
using System.Collections;
using System.Collections.Generic;
using GXPEngine.SceneStuff;


namespace GXPEngine
{
    public class Scene : GameObject
    {
        public List<Layer> layers;
        public new string name;

        public Scene(int layerCount)
        {
            layers = new List<Layer>(layerCount);

            for (var i = 0; i < layerCount; i++) layers.Add(new Layer());
            foreach (Layer layer in layers) this.AddChild(layer);
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