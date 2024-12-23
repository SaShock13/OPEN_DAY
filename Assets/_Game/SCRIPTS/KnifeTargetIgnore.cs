using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTargetIgnore : MonoBehaviour
{
    [SerializeField] private BoxCollider peakCollider;
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("KnifeTarget"))
        {
            Debug.Log("Knife in");
            //transform.parent = collision.transform;
            if (collision.contacts[0].otherCollider == peakCollider)
            {
                Physics.IgnoreCollision(peakCollider, collision.contacts[0].thisCollider, true);
            }

        }
    }
}
