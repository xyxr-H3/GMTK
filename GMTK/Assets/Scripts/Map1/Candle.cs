using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField]
    GameObject candlelight;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Animator animator;
    Vector3 ctoPPosition;
    float distance = 2f;
    bool isMove;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //RecordPosition();
        if (Input.GetKey(KeyCode.Space) && isMove)
        {
            candlelight.SetActive(false);
            this.enabled = false;
            animator.SetBool("IsFall",true);
        }
    }
    void RecordPosition()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < distance)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //ctoPPosition = this.transform.position - player.transform.position;
                //isMove = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMove = true;
            Debug.Log(2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMove = false;
            Debug.Log(3);
        }
    }
}
