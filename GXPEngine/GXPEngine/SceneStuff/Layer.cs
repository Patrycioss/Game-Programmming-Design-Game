using System;
using System.Collections.Generic;

namespace GXPEngine.SceneStuff
{
    public class Layer : GameObject
    {
        public Dictionary<string, GameObject> gameObjects;
        private List<string> toBeRemoved;
        private List<GameObject> toBeAdded;

        public Layer()
        {
            gameObjects = new Dictionary<string, GameObject>();
            toBeRemoved = new List<string>();
            toBeAdded = new List<GameObject>();
        }

        public void Update()
        {
            if (toBeRemoved.Count > 0)
            {
                foreach (string objectName in toBeRemoved)
                {
                    gameObjects[objectName].Remove();
                    gameObjects.Remove(objectName);
                }
                toBeRemoved.Clear();
            }

            if (toBeAdded.Count > 0)
            {
                foreach (GameObject gameObject in toBeAdded)
                {
                    gameObjects.Add(gameObject.name, gameObject); 
                    AddChild(gameObject);
                }
                toBeAdded.Clear();
            }
        }

        public void LateAddObject(GameObject gameObject)
        {
           toBeAdded.Add(gameObject);
        }

        public void AddObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject.name, gameObject);
            AddChild(gameObject);
        }
        
        public void LateRemoveObject(string objectName)
        {
            toBeRemoved.Add(objectName);
        }

        public void RemoveObject(string objectName)
        {
            gameObjects[objectName].Remove();
            gameObjects.Remove(objectName);
        }

        public Dictionary<string,GameObject> ListOfObjects()
        {
            return gameObjects;
        }
    }
}