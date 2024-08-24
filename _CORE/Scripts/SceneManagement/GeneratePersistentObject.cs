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
            if (CheckForOtherPersistentObjects())
            {
                Destroy(this.gameObject);
                return;
            }

            if (!_persistentObjectSpawned)
            {
                _persistentObjectSpawned = Instantiate(_persistentObjectPrefab, Vector3.zero, Quaternion.identity, transform);
            }

            if (!string.IsNullOrEmpty(_sceneName))
            {
                SceneManager.LoadScene(_sceneName);
            }
        }

        private bool CheckForOtherPersistentObjects()
        {
            var persistentObjects = FindObjectOfType<GeneratePersistentObject>();

            if(persistentObjects != this)
            {
                return true;   
            }
            return false;
        }
    }
}
