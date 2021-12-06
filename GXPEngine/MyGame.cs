using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Runtime.InteropServices;
using TiledMapParser; // System.Drawing contains drawing tools such as Color definitions
using GXPEngine.Core;
using GXPEngine.Objects;

public class MyGame : Game
{
	public Texture2D square;
	
	public Camera camera;
	public EasyDraw canvas;

	public Mouse mouse;

	private Square sq;
	
	
	public MyGame() : base(950, 550, true, false, 1900, 1100, true)
	{
		square = new Texture2D("square.png");
		
		camera = new Camera(0, 0, width, height);
		mouse = new Mouse();

		sq = new Square(square, 0, 0);
		
		
		
		
		AddChild(sq);
		AddChild(camera);
		AddChild(mouse);

	}

	
	void Update()
	{
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}