#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    [System.Serializable]
    public class Cel
    {
        [SerializeField] public Sprite[] Sprites;
        [SerializeField] public Direction Direction;

        public Sprite Resolve(int index)
        {
            return Sprites[index];
        }
    }
}
