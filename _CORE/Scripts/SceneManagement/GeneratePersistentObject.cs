using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    internal class GeneratePersistentObject : MonoBehaviour
    {
        [SerializeField] private GameObject _persistentObjectPrefab;
        [SerializeField] private GameObject _persistentObjectSpawned;
        [SerializeField] private string _sceneName;

        private void Start()
        {
            CheckForOtherPersistentObjects();

            if (!_persistentObjectSpawned)
            {
                _persistentObjectSpawned = Instantiate(_persistentObjectPrefab, Vector3.zero, Quaternion.identity, transform);
            }

            if (!string.IsNullOrEmpty(_sceneName))
            {
                SceneManager.LoadScene(_sceneName);
            }
        }

        private void CheckForOtherPersistentObjects()
        {
            var persistentObjects = FindObjectOfType<GeneratePersistentObject>();

            if( persistentObjects)
            {
                Destroy(this);
            }
        }
    }
}
