using System;
using System.Collections;
using System.Collections.Generic;
using GXPEngine.Core;

namespace GXPEngine.Enemies
{
    public class Enemy : Entity
    {
        //Base class for all Enemies, used to only check for enemy objects for the player to get damage from
        
        protected Collision collision;

        public Enemy(string filePath, int columns, int rows, int frames) : base(filePath, columns, rows, frames)
        {
            collider.isTrigger = false;
        }

        public override void Update(){}
    }
}