using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZKY
{
    public class SceneTransSetter : MonoBehaviour
    {
        [SerializeField] private string _sceneToGo;
        [SerializeField] private bool _useTransition;
        private bool _isTransitioning = false;


        private void Update()
        {
            //任意键按下
            if (Input.anyKeyDown && !_isTransitioning)
            {
                _isTransitioning = true;
                SceneLoader.instance.LoadScene(_sceneToGo, _useTransition);
            }
        }
    }
}
