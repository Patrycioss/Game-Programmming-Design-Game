﻿using System;
using GXPEngine.Core;

namespace GXPEngine
{
    public class Finish : Sprite
    {
        private Vector2 destination;

        private Enum nextStage;
        
        public Finish(string imagePath, float x, float y, Enum nextStage) : base(imagePath, false, true)
        {
            this.x = x;
            this.y = y;
            this.nextStage = nextStage;
            
            _myGame = (MyGame) game;

            collider.isTrigger = true;

            destination = new Vector2(_myGame.startPosition.x, _myGame.startPosition.y);
        }

        public void SetPlayerDestination(float x, float y)
        {
            destination.Set(x,y);
        }

        void Update()
        {
            if (HitTest(_myGame.player))
            { 
               _myGame.StageCreator.SetStage(nextStage);
               _myGame.player.SetXY(destination.x,destination.y);
            }
        }
    }
}