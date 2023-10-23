#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    [CreateAssetMenu(fileName = "New Animation Node", menuName = "RetroAnimator/Nodes/Animation", order = 400)]
    public class AnimationNode : ScriptableObject
    {
        [SerializeField] private Cel[] cels;

        public virtual AnimationNode Resolve(int index)
        {
            return this;
        }

        public virtual Cel ResolveCel(int index)
        {
            return cels[index];
        }
    }
}
