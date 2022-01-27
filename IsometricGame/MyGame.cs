using GXPEngine.Entities;
using GXPEngine.Extras;
using GXPEngine.StageManagement;
using GXPEngine.UserInterface;
using GXPEngine.UserInterface.Menu;

namespace GXPEngine
{
	public class MyGame : Game
	{
		public Player player;
		public readonly Menu menu;
		public Hud hud;

		private Sound music;
		private float volume;

		//From what x does the screen start scrolling
		private readonly int scrollX;
	
		private MyGame() : base(1920, 1080, true, true, -1, -1, true)
		{
			//From what point onwards the screen starts scrolling with the player
			scrollX = width / 2;
		
			//Initializing
			menu = new Menu();
			player = new Player();
			hud = new Hud();

			music = new Sound("sounds/music.mp3", true, true);
			
			//Background	
			Background currentBackground = new Background("backgrounds/standard.png");

			AddChild(currentBackground);
			AddChild(menu);

			volume = 0.2f;
			music.Play(volume:volume);
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
}