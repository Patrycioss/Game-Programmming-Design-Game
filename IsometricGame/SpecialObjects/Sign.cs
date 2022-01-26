
namespace GXPEngine.SpecialObjects
{
    public class Sign : Sprite
    {
        private readonly EasyDraw canvas;

        public Sign(string text) : base("sprites/misc/sign.png")
        {
            collider.isTrigger =  true;
            
            canvas = new EasyDraw(1000,100, false);
            canvas.SetXY(-32,-32);
            
            canvas.Fill(0);
            canvas.TextSize(16);
            canvas.Text(text,0,32);
            AddChild(canvas);
            
        }

        void Update()
        {
            canvas.visible = (HitTest(_myGame.player));
        }
    }
}