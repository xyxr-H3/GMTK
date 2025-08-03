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
    public GameObject lockThis;
    Quaternion initialAngle;
    bool isRotate;
    bool isReduce;
    float timeCount;
    float timeCount2 = 0;
    Vector3 temp = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        mainWorldBox = GameObject.Find("MainWorldBox");
        temp = mainWorldBox.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        BoxRotateFunction();
        LockFunction();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (!isRotate)
            {
                initialAngle = mainWorldBox.transform.rotation;
                timeCount = 0;
                timeCount2 = 0;
                Debug.Log(initialAngle.eulerAngles.y);
                isRotate = true;
                isReduce = true;
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
                mainWorldBox.transform.localScale = new Vector3(1, 1, 1);
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
        if (isReduce)
        {
            timeCount2 += Time.deltaTime;
            float t1 = Mathf.Clamp(timeCount2 / 0.5f, 0f, 1f);
            mainWorldBox.transform.localScale = Vector3.Lerp(temp, temp * 0.8f, t1);
            if (t1 == 1)
            {
                float t2 = Mathf.Clamp(timeCount2 / 1f, 0f, 1f);
                mainWorldBox.transform.localScale = Vector3.Lerp(temp * 0.8f, temp, t2);
                if (t2 == 1)
                {
                    isReduce = false;
                    timeCount2 = 0;
                }
            }
        }
        
    }
    void LockFunction()
    {
        if (lockThis.activeSelf)
        {
            this.GetComponent<Collider>().isTrigger = false;
        }
        else
        {
            this.GetComponent<Collider>().isTrigger = true;
        }
    }
}
