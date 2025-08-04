using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ZKY;

public class Motion : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    Animator animator;
    float x;
    float y;
    float z;
    [SerializeField]
    MyEvents mEvents;
    bool isDead = false;
    float t;
    [SerializeField]
    GameObject worldBox;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            AnimatorManager();
        }
        else
        {
            t -= Time.deltaTime;
            if (t < 0)
            {
                animator.SetBool("IsDead", false);
                this.transform.position = new Vector3(-4, 0.5f, -2.5f);
                worldBox.transform.eulerAngles = new Vector3(0, 0, 0);
                isDead = false;
            }
        }
    }
    void Move()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(x, y, 0) * speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.y - 3, -6, -0.1f));
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
    private void OnEnable()
    {
        mEvents._event += OnDead;
    }

    private void OnDisable()
    {
        mEvents._event -= OnDead;
    }

    void OnDead()
    {
        rb.velocity = Vector3.zero;
        isDead = true;
        t = 1;
        animator.SetBool("IsDead", true);
    }
}
