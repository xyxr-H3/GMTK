using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 场景加载器类，继承自SinglatonForMono<SceneLoader>
/// </summary>
public class SceneLoader : SinglatonForMono<SceneLoader>
{
    /// <summary>
    /// 当前场景的名称
    /// </summary>
    [SerializeField] private string currentScene;
    [SerializeField] private RawImage _transImage;
    [SerializeField] private float _tranTime;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

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
            _transImage.DOColor(Color.black, _tranTime).SetEase(Ease.InOutQuad);
        }

        yield return new WaitForSeconds(_tranTime);

        yield return SceneManager.LoadSceneAsync(sceneToGo, LoadSceneMode.Additive);

        yield return SceneManager.UnloadSceneAsync(currentScene);

        currentScene = sceneToGo;

        if (useTransition)
        {
            _transImage.DOColor(Color.clear, _tranTime).SetEase(Ease.InOutQuad);
        }
    }
}
