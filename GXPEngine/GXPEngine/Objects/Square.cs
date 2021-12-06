using GXPEngine.Core;


namespace GXPEngine.Objects
{
    public class Square : Sprite
    {
        public Square(Texture2D sprite, float x, float y) : base(sprite, true)
        {
            this.x = x;
            this.y = y;
        }
        void Update(){}
    }
    
    
}