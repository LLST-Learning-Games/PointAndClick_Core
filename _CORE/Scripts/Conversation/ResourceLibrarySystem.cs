using System;
using UnityEngine;
using Yarn.Unity;

namespace Conversation
{
    public class ResourceLibrarySystem : GameSystem
    {
        [SerializeField] private CharacterDataLibrary _characterDataLibrary;

        public CharacterData GetCharacterData(string id) => 
            _characterDataLibrary.GetCharacterData(id);

        private void Reset()
        {
            FindCharacterDataManager();
        }

        [ContextMenu("Find CharacterDataLibrary")]
        private void FindCharacterDataManager()
        {
            _characterDataLibrary = gameObject.GetComponent<CharacterDataLibrary>();
        }
    }
}