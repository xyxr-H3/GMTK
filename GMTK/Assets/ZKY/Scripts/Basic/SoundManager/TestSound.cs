using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        SoundManager.instance.Play("BGM");
        SoundManager.instance.FadeVolumn("BGM", 0, 1);
        // yield return new WaitForSeconds(0.2f);
        // SoundManager.instance.FadeVolumn("BGM", 1, 1);
        yield return new WaitForSeconds(2);
    }
}
