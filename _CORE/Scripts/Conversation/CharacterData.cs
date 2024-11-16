using UnityEngine;

namespace Conversation
{
    [CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
    public class CharacterData : ScriptableObject
    {
        public string Id;
        public Color ActiveTextColor;
        public Color HistoricTextColor;

        //todo:
        public Sprite CharacterSprite;
        public Color NameColor;
    }
}
