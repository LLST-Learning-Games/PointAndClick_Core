using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class GameSystem : MonoBehaviour
{
    [SerializeField] protected string _systemId;
    public virtual string GetId() => _systemId;
}
