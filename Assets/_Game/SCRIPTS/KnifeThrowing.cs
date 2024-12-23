using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class KnifeThrowing : MonoBehaviour

{
    private Rigidbody rb;
    private Transform initParent;
    [SerializeField] private BoxCollider peakCollider;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        //initParent = transform.parent;

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("KnifeTarget"))
    //    {
    //        Debug.Log("Knife in");
    //        //transform.parent = collision.transform;
    //        if (other.   .contacts[0].thisCollider == peakCollider)
    //        {

    //            Debug.Log("PEAK in");
    //            rb.isKinematic = true;
    //        }

    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
        
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Something in");
        if (collision.gameObject.CompareTag("KnifeTarget"))
        {
            Debug.Log("Knife in");
            //transform.parent = collision.transform;
            if (collision.contacts[0].thisCollider == peakCollider)
            {
                
                Debug.Log("PEAK in");
                rb.isKinematic = true;
            }
            
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Something out");
        if (collision.transform.CompareTag("KnifeTarget"))
        {
            Debug.Log("Knife out");
            //transform.parent = initParent;
            //rb.isKinematic = false;
        }
    }
}
