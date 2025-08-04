using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZKY;

public class Spider : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    GameObject player;
    [SerializeField]
    MyEvents events;
    float t = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            rb.velocity = (player.transform.position - this.transform.position);
        }

        if (player.transform.position.x > this.transform.position.x)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            t = 1;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        t -= Time.deltaTime;
        if (other.CompareTag("Player"))
        {
            if (t < 0)
            {
                events.Invoke();
                this.gameObject.SetActive(false);
            }
        }
    }
}
