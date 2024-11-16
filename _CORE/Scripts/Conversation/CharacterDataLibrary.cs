using System.Collections.Generic;
using UnityEngine;

namespace Conversation {
    public class CharacterDataLibrary : MonoBehaviour
    {
        [SerializeField] private List<CharacterData> _characterData;
        private Dictionary<string, CharacterData> _characterDataDictionary = new ();
        private void Awake()
        {
            GenerateCharacterDataDictionary();
        }

        private void GenerateCharacterDataDictionary()
        {
            foreach (var characterData in _characterData)
            {
                if (_characterDataDictionary.ContainsKey(characterData.Id))
                {
                    Debug.LogError($"[{GetType().Name}] More than one character found with Id {characterData.Id}");
                }
                _characterDataDictionary[characterData.Id] = characterData;
            }
        }

        public CharacterData GetCharacterData(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            if(_characterDataDictionary.TryGetValue(id, out var data))
            {
                return data;
            }

            Debug.LogWarning($"[{GetType().Name}] No character found with Id {id}");
            return null;
        }
    } 
}
