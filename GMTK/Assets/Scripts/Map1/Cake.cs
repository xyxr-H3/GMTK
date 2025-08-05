using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZKY;

public class Cake : MonoBehaviour
{
    bool isrun;
    [SerializeField]
    MyEvents mEvents;
    [SerializeField] private GameObject _infoTag;
    private bool _isInteract;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isrun && Input.GetKeyDown(KeyCode.Space) && !_isInteract)
        {
            mEvents.Invoke();
            _isInteract = true;
            _infoTag.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isInteract)
        {
            isrun = true;
            _infoTag.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !_isInteract)
        {
            isrun = false;
            _infoTag.SetActive(false);
        }
    }
}
