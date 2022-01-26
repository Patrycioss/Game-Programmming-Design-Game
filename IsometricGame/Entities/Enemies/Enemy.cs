using GXPEngine.Core;

namespace GXPEngine.Entities.Enemies
{
    /// <summary>
    /// Base class for all enemies
    /// </summary>
    public class Enemy : Entity
    {
        protected Enemy(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            collider.isTrigger = true;
        }
    }
}