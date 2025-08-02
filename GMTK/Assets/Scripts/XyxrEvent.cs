using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Death",menuName ="Xyxr/Death")]
public class XyxrEvent : ScriptableObject
{
    public void Death(GameObject player)
    {
        player.transform.position = new Vector3(-4,0.5f,-2.5f);
    }
}
