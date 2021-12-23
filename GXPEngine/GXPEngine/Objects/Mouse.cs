namespace GXPEngine.Objects
{
    public class Mouse : Sprite
    {
        public Mouse() : base("cursor.png", true, false)
        {
            width = 30;
            height = 45;
        }

        void Update()
        {
            this.x = Input.mouseX;
            this.y = Input.mouseY;
            
        }
        
        
    }
}