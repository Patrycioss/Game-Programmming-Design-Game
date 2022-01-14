using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Drawing.Design; // System.Drawing contains drawing tools such as Color definitions
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Xml.Schema;
using TiledMapParser; 
using GXPEngine.Core;


public class MyGame : Game
{
	public StageCreator StageCreator;
	public StageLoader StageLoader;
	public Background currentBackground;
	public Player player;
	public Menu menu;
	public Hud hud;
	
	public Vector2 startPosition;
	
	public MyGame() : base(1900, 1080, false, true, -1, -1, true)
	{
		//Initialzing
		StageLoader = new StageLoader();
		StageCreator = new StageCreator();

		player = new Player(startPosition.x, startPosition.y);
		
		menu = new Menu();
		hud = new Hud();
		
		//Temp solution for animations		
		targetFps = 5;
		
		//Background	
		currentBackground = new Background("backgrounds/standard.png");
		
		//Standard startposition of player in a stage
		startPosition = new Vector2(150, 800);
		
		AddChild(currentBackground);

		AddChild(menu);

	}
	
	void Update()
	{
		Scroll();



		if (Input.GetKey(Key.K))
		{
			player.Damage(3);
		}
		
	}

	void Scroll()
	{
		int boundary = width / 2;
		if (player.x + StageLoader.stageContainer.x < boundary)
		{
			StageLoader.stageContainer.x = boundary - player.x;
		}
		else if (player.x + StageLoader.stageContainer.x > width - boundary)
		{
			StageLoader.stageContainer.x = width - boundary - player.x;
		}

		if (StageLoader.stageContainer.x > 0)
		{
			StageLoader.stageContainer.x = 0;
		}
		
	}
	
	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}