using System.Collections.Generic;

namespace GXPEngine.SceneStuff
{
    public class Layer : GameObject
    {
        public List<GameObject> gameObjects;
        private List<GameObject> removeObjects;

        public Layer()
        {
            gameObjects = new List<GameObject>();
            removeObjects = new List<GameObject>();
        }

        public void Update()
        {
            if (removeObjects.Count > 0)
            {
                foreach (Sprite sprite in removeObjects)
                {
                    gameObjects.Remove(sprite);
                }
                removeObjects.Clear();
            }
        }

        public void AddObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            this.AddChild(gameObject);
        }

        public void RemoveObject(GameObject gameObject)
        {
            removeObjects.Add(gameObject);
        }

        public List<GameObject> ListOfObjects()
        {
            return gameObjects;
        }
    }
}