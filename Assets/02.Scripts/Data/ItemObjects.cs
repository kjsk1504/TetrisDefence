using System;
using System.Collections.Generic;
using TetrisDefence.Enums;
using UnityEngine;

namespace TetrisDefence.Data
{
    public class ItemObjects : PooledObjects
    {
        private void Awake()
        {
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    if (Enum.IsDefined(typeof(EItem), child.name))
                    {
                        child.SetSiblingIndex((int)Enum.Parse(typeof(EItem), child.name));
                    }
                    else
                    {
                        child.gameObject.SetActive(false);
                        Destroy(child.gameObject);
                    }
                }
            }

            transforms = new List<Transform>(transform.GetComponentsInChildren<Transform>());
            transforms.Remove(transform);

            var childNames = transforms.ConvertAll(x => x.name);
            var itemNames = new List<string>(Enum.GetNames(typeof(EItem)));

            if (childNames != itemNames)
            {
                if (childNames.Count > itemNames.Count)
                {
                    for (int ix = 0; ix < childNames.Count; ix++)
                    {
                        if (ix < itemNames.Count)
                        {
                            transforms[ix].name = itemNames[ix];
                        }
                        else
                        {
                            transforms[ix].gameObject.SetActive(false);
                            Destroy(transforms[ix].gameObject);
                        }
                    }
                }
                else
                {
                    for (int ix = 0; ix < itemNames.Count; ix++)
                    {
                        if (ix < childNames.Count)
                        {
                            transforms[ix].name = itemNames[ix];
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

                transforms = new List<Transform>(transform.GetComponentsInChildren<Transform>());
                transforms.Remove(transform);
                transforms.RemoveAll(x => !x.gameObject.activeSelf);
            }
        }
    }
}
