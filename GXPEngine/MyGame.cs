using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Runtime.InteropServices;
using TiledMapParser; // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.Core;
using GXPEngine.GXPEngine.AddOns;
using GXPEngine.GXPEngine.SceneStuff;
using GXPEngine.Objects;
using Layer = GXPEngine.SceneStuff.Layer;

public class MyGame : Game
{
	public Camera camera;
	public Mouse mouse;
	public EasyDraw canvas;
	
	public SceneManager sceneManager;
	private MakeScene makeScene;
	
	
	public MyGame() : base(950, 550, true, false, 1900, 1100, true)
	{
	//Initialize special thingies	
		camera = new Camera(0, 0, width, height);
		mouse = new Mouse();
		canvas = new EasyDraw(width, height);
		sceneManager = new SceneManager();
		makeScene = new MakeScene(width, height);
		
		
		canvas.Rect(50,50,100,100);
		
	//Making Scenes	
		Scene newScene;

		
		//Make a starting scene
		newScene = makeScene.Start();
		sceneManager.AddScene(newScene.name,newScene);
		
		
	//Drawing scene	
		//Set the scene currently displayed
		sceneManager.SetCurrentScene(sceneManager.scenes["Start"]);

		for (int i = 0; i < sceneManager.currentScene.layers.Count; i++)
		{
			foreach (GameObject gameObject in sceneManager.currentScene.layers[i].gameObjects)
			{
				Console.WriteLine(gameObject);
				Console.WriteLine(gameObject.x);
				Console.WriteLine(gameObject.y);
				Console.WriteLine(gameObject.visible);
				Console.WriteLine(gameObject.name);
				Console.WriteLine(gameObject.scaleX);
				Console.WriteLine(gameObject.InHierarchy());
				
				AddChild(gameObject);
			}
			
		}
		

	//Add Special thingies	
		AddChild(canvas);
		AddChild(camera);
		AddChild(mouse);
	}


	void Update()
	{
		// Console.WriteLine(Input.mouseX + ", "+ Input.mouseY);
		
	}
	
	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}