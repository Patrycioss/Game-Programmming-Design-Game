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

	//From what x does the screen start scrolling
	private int scrollX;
	
	public MyGame() : base(1900, 1080, true, true, -1, -1, true)
	{
		scrollX = width / 2;
		
		//Initialzing
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

		
		//DEBUG
	
		//Kills the player
		if (Input.GetKey(Key.K))
		{
			player.Damage(3);
		}
		
		//Damages the player for 1 hp
		if (Input.GetKey((Key.J)))
		{
			player.Damage(1);
		}
		
		//Toggle player hitbox
		if (Input.GetKey(Key.H))
		{
			player.ToggleHitBox();
		}
		
	}

	void Scroll()
	{
		if (player != null && StageLoader.currentStage != null)
		{
			
			//If the player is to the left of the center of the screen it will move to the left with the player until it hits the start of the stage
			if (player.x + StageLoader.currentStage.x < scrollX)
			{
				StageLoader.currentStage.x = scrollX - player.x;
			}
			else if (player.x + StageLoader.currentStage.x > width - scrollX)
			{
				StageLoader.currentStage.x = width - scrollX - player.x;
			}
		
			//If the player is to the right of the center of the screen it will move to the right with the player until it hits the end of the stage
			if (StageLoader.currentStage.x > 0)
			{
				StageLoader.currentStage.x = 0;
			}
			else if (StageLoader.currentStage.x < -StageLoader.currentStage.stageWidth + game.width)
			{
				StageLoader.currentStage.x = -StageLoader.currentStage.stageWidth + game.width;
			}
		}
	}
	
	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}