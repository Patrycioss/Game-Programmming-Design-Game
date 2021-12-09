using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;							// System.Drawing contains drawing tools such as Color definitions
using System.Runtime.InteropServices;
using TiledMapParser; 
using GXPEngine.Core;
using GXPEngine.GXPEngine.AddOns;
using GXPEngine.GXPEngine.SceneStuff;
using GXPEngine.Objects;

public class MyGame : Game
{
	public Camera camera;
	public Mouse mouse;
	public EasyDraw canvas;
	public Player player;

	public bool done = false;
	
	
	public MyGame() : base(950, 550, true, false, 1900, 1100, true)
	{
	//Initialize special thingies	
		camera = new Camera(0, 0, width, height);
		mouse = new Mouse();
		canvas = new EasyDraw(width, height);
		player = new Player(0, height/2.0f - 122);
		
		
	//Making Scenes	
		Scene newScene;
		
		//Make a starting scene
		newScene = MakeScene.Start();
		SceneManager.AddScene(newScene.name,newScene);
		
		
		
		
	//Drawing scene	
		//Set the scene currently displayed
		SceneManager.SetScene("Start");
		


	//Add Special thingies	
		AddChild(canvas);
		AddChild(SceneManager.currentScene);
		AddChild(camera);
		AddChild(mouse);


		Console.WriteLine(SceneManager.scenes["Start"].layers[3].gameObjects.ContainsKey("s1"));
		
		SceneManager.currentScene.layers[0].AddObject(player);


	}


	void Update()
	{
		// Console.WriteLine(Input.mouseX + ", "+ Input.mouseY);

		// Console.WriteLine(SceneManager.scenes["Start"].layers[3].gameObjects["s1"]);
		if (Input.GetKey(Key.G) && !done)
		{
			SceneManager.scenes["Start"].MoveFromLayerToOther("s1",3,1);
			done = true;
		}
		
		
	}
	
	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}