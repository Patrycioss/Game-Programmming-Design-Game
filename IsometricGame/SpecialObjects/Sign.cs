
 using System;

 namespace GXPEngine.SpecialObjects
 {
     /// <summary>
     /// Sign object with text when player walks over it
     /// </summary>
     public class Sign : Sprite
     {
         private readonly EasyDraw canvas;

         public Sign(string text) : base("sprites/misc/sign.png")
         {
             collider.isTrigger =  true;

             
             canvas = new EasyDraw(1000,100, false);
             canvas.SetXY(-200,-32);
             
             canvas.Fill(0);
             canvas.TextSize(16);
             canvas.TextAlign(CenterMode.Center,CenterMode.Max);
             canvas.Text(text,230,32);
             AddChild(canvas);
         }

         void Update()
         {
             canvas.visible = (HitTest(myGame.player));

             if (HitTest(myGame.player))
             {
                 Console.WriteLine("ha");
             }
         }
     }
 }