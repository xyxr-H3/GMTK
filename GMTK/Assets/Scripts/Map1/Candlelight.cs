using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candlelight : MonoBehaviour
{
    [SerializeField] float distance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = new Vector3(collision.transform.position.x - distance, collision.transform.position.y, collision.transform.position.z);
            Debug.Log(1);
        }
    }
}
