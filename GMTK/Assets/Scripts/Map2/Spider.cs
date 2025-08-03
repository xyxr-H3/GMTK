using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    GameObject player;
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
}
