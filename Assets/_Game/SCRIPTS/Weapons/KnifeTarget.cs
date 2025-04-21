using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTarget : MonoBehaviour
{

    private Rigidbody knifeRb;
    private bool isInTarget = false;


    /// <summary>
    /// Несколько ноже сделать , чтобы можно было вынимать из мишени.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("InTrigger");
        if (!isInTarget)
        {
            if (other.CompareTag("KnifePeak"))
            {
                print("Knife is in the target");
                isInTarget = true;
                //knifeRb = other.gameObject.GetComponentInParent<Rigidbody>();
                //if (knifeRb.isKinematic == false)
                //{
                //    knifeRb.isKinematic = true;
                //}
            } 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //knifeRb.isKinematic=false;
        isInTarget = false;
    }
}
