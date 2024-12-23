using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DoorHandle : MonoBehaviour
{
    [SerializeField] private bool isBack = false;
    private CircularDrive circularDrive;

    private float startRotationY;



    [SerializeField] private float springSpeed =2 ;

    private void Start()
    {
        startRotationY = transform.rotation.eulerAngles.y;
        circularDrive = GetComponent<CircularDrive>();
    }

    private void OnCollisionExit(Collision collision)
    {
        isBack = true;
    }

    private void Update()
    {
        if (isBack)
        {
            SpringBack();

            print($"angle : {transform.rotation.eulerAngles.y}");
        }

        //if (transform.rotation.eulerAngles.y == startRotationY )
        //{
        //    isBack = false;
        //}
    }

    public void SpringBack()
    {
        Debug.Log("Spring");
        circularDrive.outAngle = 30;
        
        //float rotationY = Mathf.Lerp(transform.rotation.eulerAngles.y, startRotationY, Time.deltaTime * springSpeed);
        //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, rotationY, transform.rotation.eulerAngles.z));
    }

    public void OnAttach()
    {
        print("");
    }
}
