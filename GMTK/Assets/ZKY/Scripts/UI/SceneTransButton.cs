using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZKY
{
    public class SceneTransButton : MonoBehaviour
    {
        private Button _button;
        [SerializeField] private string _sceneName;
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() =>
            {
                SceneLoader.instance.LoadScene(_sceneName);
            });
        }
    }
}
