
namespace GXPEngine.Extras
{
    public class Timer : GameObject
    {
        public bool finished;
        private readonly int length;
        private readonly int startTime;
        
        public Timer(int length)
        {
            finished = false;
            this.length = length;
            startTime = Time.now;
            game.AddChild(this);
        }

        void Update()
        {
            if (startTime + length < Time.now)
            {
                finished = true;
                this.Destroy();
            }
        }
        
    }
}