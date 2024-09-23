using SystemManagement;
using UnityEngine;

namespace Persistence 
{
    public class EnabledPersistence : MonoBehaviour
    {
        private const string ENABLED_PERSISTENCE_SUFFIX = "_enabled";
        private ScenePersistenceSystem _persistenceSystem;

        private string _key => gameObject.name + ENABLED_PERSISTENCE_SUFFIX;

        private void Start()
        {
            _persistenceSystem = GameSystemManager.Instance.GetSystem<ScenePersistenceSystem>("ScenePersistence");
            if(!_persistenceSystem.TryGetPersistentObject<bool>(_key, out var isEnabled))
            {
                isEnabled = true;   // if we haven't yet registered, assume it is enabled
            }

            gameObject.SetActive(isEnabled);
        }

        public void RegisterActiveState(bool state)
        {
            _persistenceSystem.RegisterPersistentObject(_key, state);
        }
    }
}