using GXPEngine.Core;
using System;

namespace GXPEngine
{
    public class Walkthrough : Sprite
    {
        // Walkthrough(string fileName, int rows, int columns, int frames) : base(fileName, rows, columns, frames)
        // {
        //     collider.isTrigger = true;
        // }

        Walkthrough(string fileName) : base(fileName)
        {
            collider.isTrigger = true;
        }
    }
}