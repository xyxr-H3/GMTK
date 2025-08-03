using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZKY;

public class Cake : MonoBehaviour
{
    bool isrun;
    [SerializeField]
    MyEvents mEvents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isrun&&Input.GetKeyDown(KeyCode.Space))
        {
            mEvents.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isrun = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isrun = false;
        }
    }
}
