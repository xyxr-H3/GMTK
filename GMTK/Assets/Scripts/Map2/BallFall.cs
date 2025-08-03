using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFall : MonoBehaviour
{
    [SerializeField]
    GameObject spider;
    [SerializeField]
    Animator animator;
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
