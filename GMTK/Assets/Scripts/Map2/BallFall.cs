using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZKY;

public class BallFall : MonoBehaviour
{
    [SerializeField]
    GameObject spider;
    [SerializeField]
    Animator animator;
    [SerializeField]
    MyEvents events;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!spider.activeSelf)
            {
                animator.SetBool("isfall", true);
                events.Invoke();
            }
            else
            {
                animator.SetBool("attackSpider", true);
                spider.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isfall", false);

        }
    }
}
