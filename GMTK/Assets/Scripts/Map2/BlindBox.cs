using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindBox : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    float distance = 2;
    int count = 0;
    int randomMax;
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
        if (Vector3.Distance(Player.transform.position, this.transform.position) < distance)
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
            bool isNull = UnityEngine.Random.Range(0, randomMax) < 0.5f;
            if (isNull && count < 3)
            {
                Debug.Log("你没中奖");
            }
            else if (!isNull || count == 3)
            {
                randomMax = 1;
                Debug.Log("你中奖了");
            }
        }
        count++;
    }

    void CreateSpider()
    {
        Debug.Log("蜘蛛跑出来了");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, distance);
    }
}
