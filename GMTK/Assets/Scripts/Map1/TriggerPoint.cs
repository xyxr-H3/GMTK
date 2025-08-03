using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoint : MonoBehaviour
{
    [SerializeField]
    GameObject candle;
    [SerializeField]
    GameObject candleLight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(candle.transform.position, this.transform.position) < 0.1f)
        {
            candle.SetActive(false);
            candleLight.SetActive(false);
        }
    }
}
