using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    Animator animator;
    float x;
    float y;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AnimatorManager();
    }
    void Move()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(x, y, 0) * speed;

    }
    void AnimatorManager()
    {
        if (rb.velocity.magnitude > 0)
        {
            animator.SetBool("IsWalk", true);
            if (x > 0)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (rb.velocity.magnitude > 0 && x < 0)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }

    }
}
