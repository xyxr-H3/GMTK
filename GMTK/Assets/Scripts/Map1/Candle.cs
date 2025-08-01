using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField]
    GameObject candlelight;
    [SerializeField]
    GameObject player;
    float distance = 1.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < distance)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                candlelight.SetActive(false);
            }
        }
    }
}
