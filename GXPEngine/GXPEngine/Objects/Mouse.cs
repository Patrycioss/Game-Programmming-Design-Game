namespace GXPEngine.Objects
{
    public class Mouse : Sprite
    {
        public Mouse() : base("cursor.png", true, false)
        {
            width = 10;
            height = 15;
        }

        void Update()
        {
            this.x = Input.mouseX;
            this.y = Input.mouseY;
        }
    }
}