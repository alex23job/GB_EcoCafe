using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal interface ITaking
    {
        public GameObject TakingItem();
        string Description { get; set; }
        int TakingID { get; }
        int TakingPrice { get; }
        public void HideItem();
    }
}
