using System;
using System.Collections;
using System.Collections.Generic;
using GXPEngine.Core;

namespace GXPEngine.Enemies
{
    public class Enemy : Entity
    {
        protected Collision _hCollision;
        protected Collision _vCollision;
        public Enemy(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            collider.isTrigger = false;
            Console.WriteLine(1);
        }

       public override void Update(){}
    }
}