using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    XyxrEvent xyxrEvent;
    float t = 3;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (t < 0)
        {
            xyxrEvent.Death(this.gameObject);
        }
    }
}
