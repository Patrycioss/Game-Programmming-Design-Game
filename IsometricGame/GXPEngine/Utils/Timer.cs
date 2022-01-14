using System;
using System.Xml.Schema;

namespace GXPEngine
{
    public class Timer : GameObject
    {
        public bool finished;
        public int length;

        private int startTime;
        
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