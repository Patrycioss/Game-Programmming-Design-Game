using GXPEngine.Core;

namespace GXPEngine.Entities.Enemies
{
    public class Enemy : Entity
    {
        //Base class for all Enemies, used to only check for enemy objects for the player to get damage from
        
        protected Collision collision;

        protected Enemy(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            collider.isTrigger = false;
        }

        public override void Update(){}
    }
}