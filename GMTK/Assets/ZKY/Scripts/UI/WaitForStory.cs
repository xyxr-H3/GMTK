using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZKY
{
    public class WaitForStory : MonoBehaviour
    {
        [SerializeField] private SceneTransSetter _sceneTransSetter;
        [SerializeField] private float _waitTime;

        private void Start()
        {
            _sceneTransSetter.enabled = false; // 禁用场景转换设置器
            StartCoroutine(WaitForStoryCoroutine()); // 启动协程
        }

        private IEnumerator WaitForStoryCoroutine()
        {
            yield return new WaitForSeconds(_waitTime); // 等待指定时间
            _sceneTransSetter.enabled = true; // 启用场景转换设置器
        }
    }
}
