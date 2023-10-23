#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    [CreateAssetMenu(fileName = "New Switch Node", menuName = "RetroAnimator/Nodes/Switch", order = 400)]
    public class SwitchNode : AnimationNode
    {
        [SerializeField] private AnimationNode[] nodes;

        public override AnimationNode Resolve(int index)
        {
            return nodes[index];
        }
    }
}
