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

    /// <summary>
    /// 加载场景的方法
    /// </summary>
    /// <param name="sceneToGo">要加载的场景名称</param>
    public void LoadScene(string sceneToGo)
    {
        if (currentScene == "")
            currentScene = SceneManager.GetActiveScene().name;

        StartCoroutine(LoadSceneCourtine(sceneToGo));
    }

    /// <summary>
    /// 加载场景的协程方法
    /// </summary>
    /// <param name="sceneToGo">要加载的场景名称</param>
    /// <returns></returns>
    private IEnumerator LoadSceneCourtine(string sceneToGo)
    {
        yield return SceneManager.LoadSceneAsync(sceneToGo, LoadSceneMode.Additive);

        yield return SceneManager.UnloadSceneAsync(currentScene);

        currentScene = sceneToGo;
    }
}
