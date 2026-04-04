using System;
using System.Collections.Generic;

namespace Assets.Scripts.Player
{
    internal interface IMyCommand
    {
        public void Execute(int numCommand);
    }
}
