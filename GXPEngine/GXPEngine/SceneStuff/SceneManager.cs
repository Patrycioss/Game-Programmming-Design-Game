using System.Collections;
using System.Collections.Generic;

namespace GXPEngine.GXPEngine.AddOns
{
    public static class SceneManager
    {
        public static Dictionary<string,Scene> scenes;
        public static Scene currentScene;
        
        static SceneManager()
        {
            scenes = new Dictionary<string, Scene>();
        }

        public static void AddScene(string name, Scene scene)
        {
            scenes.Add(name,scene);
        }

        public static void SetScene(string sceneName)
        {
            currentScene = scenes[sceneName];
        }
    }
}