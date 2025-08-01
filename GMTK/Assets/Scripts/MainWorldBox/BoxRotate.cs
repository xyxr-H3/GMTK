using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxRotate : MonoBehaviour
{
    public LayerMask mask;
    public GameObject mainWorldBox;
    public float aimAngle1;
    public float aimAngle2;
    Quaternion initialAngle;
    bool isRotate;
    float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        mainWorldBox = GameObject.Find("MainWorldBox");
    }

    // Update is called once per frame
    void Update()
    {
        BoxRotateFunction();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (!isRotate)
            {
                initialAngle = mainWorldBox.transform.rotation;
                timeCount = 0;
                Debug.Log(initialAngle.eulerAngles.y);
                isRotate = true;
            }
        }
    }
    void BoxRotateFunction()
    {
        if (initialAngle.eulerAngles.y == aimAngle2)
        {
            if (isRotate)
            {
                timeCount += Time.deltaTime;
                mainWorldBox.transform.rotation = Quaternion.Slerp(initialAngle, Quaternion.Euler(0, aimAngle1, 0), timeCount);
            }
            if (Mathf.Abs(mainWorldBox.transform.eulerAngles.y - aimAngle1) < 1)
            {
                mainWorldBox.transform.eulerAngles = new Vector3(0, aimAngle1, 0);
                isRotate = false;
            }
        }
        else if (initialAngle.eulerAngles.y == aimAngle1)
        {
            if (isRotate)
            {
                timeCount += Time.deltaTime;
                mainWorldBox.transform.rotation = Quaternion.Slerp(initialAngle, Quaternion.Euler(0, aimAngle2, 0), timeCount);
            }
            if (Mathf.Abs(mainWorldBox.transform.eulerAngles.y - aimAngle2) < 1)
            {
                mainWorldBox.transform.eulerAngles = new Vector3(0, aimAngle2, 0);
                isRotate = false;
            }
        }
    }
}
