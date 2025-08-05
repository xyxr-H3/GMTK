using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ZKY;

public class Candle : MonoBehaviour
{
    [SerializeField]
    GameObject candlelight;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject l;
    [SerializeField]
    MyEvents events;
    [SerializeField] private GameObject _infoTag;
    Vector3 ctoPPosition;
    float distance = 2f;
    bool isMove;
    private bool _isInteract;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //RecordPosition();
        if (Input.GetKey(KeyCode.Space) && isMove && !_isInteract)
        {
            candlelight.SetActive(false);
            l.SetActive(false);
            _isInteract = true;
            _infoTag.SetActive(false);
            animator.SetBool("IsFall", true);
            events.Invoke();
            this.enabled = false;
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
        if (other.CompareTag("Player") && !_isInteract)
        {
            isMove = true;
            _infoTag.SetActive(true);
            Debug.Log(2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !_isInteract)
        {
            isMove = false;
            _infoTag.SetActive(false);
            Debug.Log(3);
        }
    }
}
