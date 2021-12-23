using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;							// System.Drawing contains drawing tools such as Color definitions
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Schema;
using TiledMapParser; 
using GXPEngine.Core;
using GXPEngine.GXPEngine.AddOns;
using GXPEngine.SceneStuff;
using GXPEngine.Objects;

public class MyGame : Game
{
	public StageLoader StageLoader;

	public EasyDraw canvas;
	public MakeScene makeScene;
	public Player player;
	
	public bool done = false;
	public bool debugging = false;


	public MyGame() : base(1900, 1080, false, false, -1, -1, true)
	{
		// makeScene = new MakeScene();
		// //Making Scenes	
		// 	Scene newScene;
		// 	
		// 	//Make a starting scene
		// 	newScene = makeScene.Start();
		// 	SceneManager.AddScene(newScene.name,newScene);
		// 	
		// //Drawing scene	
		// 	//Set the scene currently displayed
		// 	SceneManager.SetScene("Start");
		// 	
		// //Add Special thingies	
		// 	AddChild(canvas);
		// 	AddChild(SceneManager.currentScene);
		//
		// 	
		// //Add the player to the currentScene	
		// 	SceneManager.currentScene.layers[0].AddObject(player);
		// 	
		// 

		
		//Initialize Stageloader
		StageLoader = new StageLoader();
		
		
		//Initialize special thingies	
		canvas = new EasyDraw(width, height, false);
		player = new Player(0,0);

		//Temp background
		canvas.HorizontalShapeAlign = CenterMode.Min;
		canvas.VerticalShapeAlign = CenterMode.Min;

		canvas.NoStroke();
		canvas.Fill(255, 0, 0);
		canvas.Rect(0, 0, width, height);

		canvas.Fill(0);
		canvas.Ellipse(0, 0, 10, 10);


		StageLoader.LoadStage("stage1/stage1.tmx");
		
		AddChild(StageLoader.stageContainer);
		AddChild(player);
		
	}


	void Update()
	{
	
		
		// //Debug mode
		// if (Input.GetKeyDown(Key.B))
		// {
		// 	SceneManager.currentScene.ToggleDebug();
		// }


				
		
	}
	
	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}