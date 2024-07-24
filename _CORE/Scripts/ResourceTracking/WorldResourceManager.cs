using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldResourceManager : MonoBehaviour
{
    [SerializeField] private List<WorldResource> _resources = new List<WorldResource>();
    
    public WorldResource GetResourceByName(string name) => _resources.First(x => x.ResourceName == name);

}
