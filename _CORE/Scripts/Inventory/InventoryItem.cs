using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class InventoryItem : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite EnvironmentSprite;
    public Sprite InventorySprite;
}

