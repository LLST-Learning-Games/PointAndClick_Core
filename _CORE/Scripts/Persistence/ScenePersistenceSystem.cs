using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ScenePersistenceDictionary = System.Collections.Generic.Dictionary<string, object>;

namespace Persistence
{
    public class ScenePersistenceSystem : GameSystem
    {
        private readonly Dictionary<string, ScenePersistenceDictionary> _persistence = new();
        private ScenePersistenceDictionary _currentSceneDictionary;
        public string LastSceneName { get; private set; }

        private void Awake()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnSceneChanged(Scene previousScene, Scene newScene)
        {
            Debug.Log($"[{GetType().Name}] Detected scene change from {previousScene.name} to {newScene.name}");
            if (!_persistence.ContainsKey(newScene.name)) 
            {
                _persistence.Add(newScene.name, new ScenePersistenceDictionary());
                Debug.Log($"[{GetType().Name}] Creating new persistence dictionary for scene {newScene.name}");
            }

            _currentSceneDictionary = _persistence[newScene.name];

            LastSceneName = previousScene.name;
        }

        public bool TryGetPersistentObject<T>(string key, out T value)
        {
            value = default(T);

            if (!_currentSceneDictionary.TryGetValue(key, out object rawValue))
            {
                Debug.Log($"[{GetType().Name}] Could not find {key} in {LastSceneName}");
                return false;
            }

            if (rawValue is not T)
            {
                Debug.Log($"[{GetType().Name}] Found key {key} as {rawValue} in scene {LastSceneName}, but it is not of type {typeof(T)}");
                return false;
            }

            value = (T)rawValue;
            Debug.Log($"[{GetType().Name}] Found key {key} as {value} in scene {LastSceneName}");

            return true;
        }

        public void RegisterPersistentObject(string key, object value)
        {
            _currentSceneDictionary[key] = value;
            Debug.Log($"[{GetType().Name}] Registered key {key} as {value} in scene {LastSceneName}");
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }
    }
}