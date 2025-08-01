using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour
{
    void Start()
    {
        SceneLoader.instance.LoadScene("New Scene");
    }
}
