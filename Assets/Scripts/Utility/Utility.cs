using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    public static class Utility
    {
        public static void Shuffle<T>(this List<T> list)
        {
            int count = list.Count;

            for (int i = 0; i < count - 1; i++)
            {
                int seed = Random.Range(i, count);

                var temp = list[i];

                list[i] = list[seed];
                list[seed] = temp;
            }
        }
    }
}
