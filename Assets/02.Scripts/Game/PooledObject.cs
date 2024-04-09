using System;
using TetrisDefence.Data.Manager;
using UnityEngine;

namespace TetrisDefence.Game
{
    public class PooledObject : MonoBehaviour
    {
        private void Awake()
        {
            string[] itemNames = Enum.GetNames(typeof(Item));
            foreach (string itemName in itemNames)
            {
                new GameObject(itemName).transform.parent = transform;
            }
        }
    }
}
