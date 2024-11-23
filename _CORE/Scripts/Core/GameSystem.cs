using System;
using UnityEngine;

[Serializable]
public abstract class GameSystem : MonoBehaviour
{
    [SerializeField] protected string _systemId;
    public virtual string GetId() => _systemId;

    public abstract void Initialize();
}
