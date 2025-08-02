using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景加载器类，继承自SinglatonForMono<SceneLoader>
/// </summary>
public class SceneLoader : SinglatonForMono<SceneLoader>
{
    /// <summary>
    /// 当前场景的名称
    /// </summary>
    [SerializeField] private string currentScene;
    [SerializeField] private Material _transMat;
    [SerializeField] private float _tranTime;

    /// <summary>
    /// 加载场景的方法
    /// </summary>
    /// <param name="sceneToGo">要加载的场景名称</param>
    public void LoadScene(string sceneToGo, bool useTransition)
    {
        if (currentScene == "")
            currentScene = SceneManager.GetActiveScene().name;

        StartCoroutine(LoadSceneCourtine(sceneToGo, useTransition));
    }

    /// <summary>
    /// 加载场景的协程方法
    /// </summary>
    /// <param name="sceneToGo">要加载的场景名称</param>
    /// <returns></returns>
    private IEnumerator LoadSceneCourtine(string sceneToGo, bool useTransition)
    {
        if (useTransition)
        {
            var timer = 0.0f;
            while (timer < _tranTime)
            {
                Debug.Log(_transMat.GetFloat("_Intensity"));
                timer += Time.deltaTime;
                _transMat.SetFloat("_Intensity", Mathf.Lerp(1.2f, -1, timer / _tranTime));
                yield return null;
            }
        }
        yield return SceneManager.LoadSceneAsync(sceneToGo, LoadSceneMode.Additive);

        yield return SceneManager.UnloadSceneAsync(currentScene);

        currentScene = sceneToGo;

        if (useTransition)
        {
            var timer = 0.0f;
            while (timer < _tranTime)
            {
                timer += Time.deltaTime;
                _transMat.SetFloat("_Intensity", Mathf.Lerp(-1, 1.2f, timer / _tranTime));
                yield return null;
            }
        }

    }
}
