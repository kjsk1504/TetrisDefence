using System;
using System.Collections.Generic;
using TetrisDefence.Data.Manager;
using UnityEngine;

namespace TetrisDefence.Game
{
    public class PooledObjects : MonoBehaviour
    {
        [field: SerializeField] List<Transform> _transforms;


        private void Awake()
        {
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    if (Enum.IsDefined(typeof(Item), child.name))
                    {
                        child.SetSiblingIndex((int)Enum.Parse(typeof(Item), child.name));
                    }
                    else
                    {
                        child.gameObject.SetActive(false);
                        Destroy(child.gameObject);
                    }
                }
            }

            _transforms = new List<Transform>(transform.GetComponentsInChildren<Transform>());
            _transforms.Remove(transform);

            var childNames = _transforms.ConvertAll(x => x.name);
            var itemNames = new List<string>(Enum.GetNames(typeof(Item)));

            if (childNames != itemNames)
            {
                if (childNames.Count > itemNames.Count)
                {
                    for (int ix = 0; ix < childNames.Count; ix++)
                    {
                        if (ix < itemNames.Count)
                        {
                            _transforms[ix].name = itemNames[ix];
                        }
                        else
                        {
                            _transforms[ix].gameObject.SetActive(false);
                            Destroy(_transforms[ix].gameObject);
                        }
                    }
                }
                else
                {
                    for (int ix = 0; ix < itemNames.Count; ix++)
                    {
                        if (ix < childNames.Count)
                        {
                            _transforms[ix].name = itemNames[ix];
                        }
                        else
                        {
                            var newItem = new GameObject(itemNames[ix]);
                            newItem.SetActive(true);
                            newItem.transform.SetParent(transform);
                            newItem.transform.SetSiblingIndex(ix);
                        }
                    }
                }

                _transforms = new List<Transform>(transform.GetComponentsInChildren<Transform>());
                _transforms.Remove(transform);
                _transforms.RemoveAll(x => !x.gameObject.activeSelf);
            }
        }
    }
}
