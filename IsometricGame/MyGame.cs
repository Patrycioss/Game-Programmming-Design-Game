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
	public Background currentBackground;
	public Player player;
	public Menu menu;
	public Hud hud;

	private int scrollBoundary;
	
	public MyGame() : base(1900, 1080, true, true, -1, -1, true)
	{
		scrollBoundary = width / 2;
		
		//Initialzing
		// player = new Player();
		menu = new Menu();
		player = new Player();
		hud = new Hud();

		//Background	
		currentBackground = new Background("backgrounds/standard.png");

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

		if (Input.GetKey(Key.H))
		{
			
		}
		
	}

	void Scroll()
	{
		if (player != null && StageLoader.currentStage != null)
		{
			if (player.x + StageLoader.currentStage.x < scrollBoundary)
			{
				StageLoader.currentStage.x = scrollBoundary - player.x;
			}
			else if (player.x + StageLoader.currentStage.x > width - scrollBoundary)
			{
				StageLoader.currentStage.x = width - scrollBoundary - player.x;
			}
		
			if (StageLoader.currentStage.x > 0)
			{
				StageLoader.currentStage.x = 0;
			}
			else if (StageLoader.currentStage.x < -StageLoader.currentStage.stageWidth + game.width)
			{
				StageLoader.currentStage.x = -StageLoader.currentStage.stageHeight + game.width;
			}
		}
	}
	
	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}