using System.Collections.Generic;
using UnityEngine;

namespace SystemManagement
{

    public class GameSystemManager : MonoBehaviour
    {
        // The system manager is our one and only singleton
        public static GameSystemManager Instance;

        [SerializeField] private List<GameSystem> _monoSystems = new();
        [SerializeField] private Canvas _uiCanvas;
        public Canvas UiCanvas => _uiCanvas;
        
        private Dictionary<string, GameSystem> _systems = new();

        [ContextMenu("FindGameSystems")]
        private void FindSystemsInScene()
        {
            _monoSystems.Clear();
            var systems = FindObjectsOfType<GameSystem>();
            _monoSystems.AddRange(systems);
        }

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
            }

            if (Instance != this)
            {
                Destroy(this);
                return;
            }

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
}