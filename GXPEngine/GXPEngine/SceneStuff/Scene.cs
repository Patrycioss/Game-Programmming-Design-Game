using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
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

            for (int i = layerCount - 1; i >= 0; i--)
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
    }
}