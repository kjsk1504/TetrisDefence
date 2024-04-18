using System;
using System.Collections.Generic;
using TetrisDefence.Data.Enums;
using UnityEngine;

namespace TetrisDefence.Data
{
    public class MinoObjects : PooledObjects
    {
        private void Awake()
        {
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    if (Enum.IsDefined(typeof(EMino), child.name))
                    {
                        child.SetSiblingIndex((int)Enum.Parse(typeof(EMino), child.name));
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
            var minoNames = new List<string>(Enum.GetNames(typeof(EMino)));

            if (childNames != minoNames)
            {
                if (childNames.Count > minoNames.Count)
                {
                    for (int ix = 0; ix < childNames.Count; ix++)
                    {
                        if (ix < minoNames.Count)
                        {
                            transforms[ix].name = minoNames[ix];
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
                    for (int ix = 0; ix < minoNames.Count; ix++)
                    {
                        if (ix < childNames.Count)
                        {
                            transforms[ix].name = minoNames[ix];
                        }
                        else
                        {
                            var newMino = new GameObject(minoNames[ix]);
                            newMino.SetActive(true);
                            newMino.transform.SetParent(transform);
                            newMino.transform.SetSiblingIndex(ix);
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
