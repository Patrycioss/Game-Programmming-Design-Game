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

	private int scrollBoundary;

	
	
	public Vector2 startPosition;
	
	public MyGame() : base(1900, 1080, false, true, -1, -1, true)
	{
		scrollBoundary = width / 2;
		
		//Initialzing
		StageLoader = new StageLoader();
		StageCreator = new StageCreator();

		player = new Player();
		hud = new Hud();

		menu = new Menu();
		
		
		//Temp solution for animations		
		// targetFps = 5;
		
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

		if (Input.GetKey((Key.J)))
		{
			player.Damage(1);
		}
		
	}

	void Scroll()
	{
		if (player.x + StageLoader.stageContainer.x < scrollBoundary)
		{
			StageLoader.stageContainer.x = scrollBoundary - player.x;
		}
		else if (player.x + StageLoader.stageContainer.x > width - scrollBoundary)
		{
			StageLoader.stageContainer.x = width - scrollBoundary - player.x;
		}
		
		if (StageLoader.stageContainer.x > 0)
		{
			StageLoader.stageContainer.x = 0;
		}

		
		
		
		else if (StageLoader.stageContainer.x < -StageLoader.mapWidth + game.width)
		{
			StageLoader.stageContainer.x = -StageLoader.mapWidth + game.width;
		}


		
	}
	
	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}