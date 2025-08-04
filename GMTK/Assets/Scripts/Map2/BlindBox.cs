using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZKY;

public class BlindBox : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject spider;
    [SerializeField]
    MyEvents encourage;
    [SerializeField]
    Vector3 spiderPostion;
    float distance = 2;
    int count = 0;
    int randomMax;
    bool isEnter = false;
    float winningRate = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Raffle();
    }
    void Raffle()
    {
        if (isEnter)
        {
            if (Input.GetKeyDown(KeyCode.Space) && count < 4)
            {
                RaffleFunction();
            }
        }
    }
    void RaffleFunction()
    {
        if (count == 0)
        {
            CreateSpider();
            randomMax = 2;
        }
        else
        {
            bool isNull = UnityEngine.Random.Range(0, randomMax) < winningRate;
            if (isNull && count < 3)
            {
                Debug.Log("你没中奖");
            }
            else if (isNull && count == 2)
            {
                winningRate = 0;
            }
            else if (!isNull)
            {
                randomMax = 1;
                winningRate = 1;
                encourage.Invoke();
                Debug.Log("你中奖了");
            }
        }
        count++;
        animator.SetInteger("count", count);
    }

    void CreateSpider()
    {
        spider.transform.localPosition = spiderPostion;
        spider.SetActive(true);
        Debug.Log("蜘蛛跑出来了");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isEnter)
        {
            isEnter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isEnter)
        {
            isEnter = false;
        }
    }
}
