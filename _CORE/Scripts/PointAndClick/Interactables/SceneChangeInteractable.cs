using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PointAndClick.Interactables
{
    public class SceneChangeInteractable : BaseInteractableBridge
    {
        [SerializeField] private int _sceneIndex;
        public override void OnInteractionExecute()
        {
            SceneManager.LoadScene(_sceneIndex);
        }
    }
}
