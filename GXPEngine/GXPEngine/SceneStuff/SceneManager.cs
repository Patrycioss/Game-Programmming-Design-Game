using System.Collections;
using System.Collections.Generic;

namespace GXPEngine.GXPEngine.AddOns
{
    public class SceneManager
    {
        public Scene currentScene;

        public Dictionary<string,Scene> scenes;
        public SceneManager()
        {
            scenes = new Dictionary<string, Scene>();
        }

        public void AddScene(string name, Scene scene)
        {
            scenes.Add(name,scene);
        }

        public void SetCurrentScene(Scene scene)
        {
            currentScene = scene;
        }
        
    }
}