using System.Collections.Generic;
using UnityEngine;

namespace Conversation {
    public class CharacterDataLibrary 
    {
        private Dictionary<string, CharacterData> _characterDataDictionary = new ();
        private void Awake()
        {
            GenerateCharacterDataDictionary();
        }

        private void GenerateCharacterDataDictionary()
        {
            // todo - lookup 
            //foreach (var characterData in characterData)
            //{
            //    if (_characterDataDictionary.ContainsKey(characterData.Id))
            //    {
            //        Debug.LogError($"[{GetType().Name}] More than one character found with Id {characterData.Id}");
            //    }
            //    _characterDataDictionary[characterData.Id] = characterData;
            //}
        }

        public CharacterData GetCharacterData(string id)
        {
            if(_characterDataDictionary.TryGetValue(id, out var data))
            {
                return data;
            }

            Debug.LogWarning($"[{GetType().Name}] No character found with Id {id}");
            return null;
        }
    } 
}
