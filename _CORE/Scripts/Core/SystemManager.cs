using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SystemManager : MonoBehaviour
{
    [SerializeField] private List<GameSystem> _monoSystems;
    private Dictionary<string, GameSystem> _systems;

    private void Awake()
    {
        foreach (var system in _monoSystems) 
        {
            _systems.Add(system.GetId(), system);
        }

    }

    public T GetSystem<T>(string id) where T : GameSystem
    {
        if (_systems.TryGetValue(id, out var system) && system is T typedSystem)
        {
            return typedSystem;
        }

        throw new GameSystemException($"[{GetType().Name}] Could not find system with id {id}");
    }
}

public class GameSystemException : System.Exception
{
    public GameSystemException(string message) : base(message)
    {
    }
}