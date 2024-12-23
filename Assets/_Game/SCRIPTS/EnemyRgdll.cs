using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRgdll : MonoBehaviour
{
    [SerializeField] private float convulsionTime = 3f;
    private Rigidbody[] rbs;
    private void Awake()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
    }

    public void StopConvulsing()
    {
        Debug.Log("StopingConvulsingMEthod");
        StartCoroutine(StopConvulsionCoroutine());
    }

    private IEnumerator StopConvulsionCoroutine()
    {
        yield return new WaitForSeconds(convulsionTime);

        foreach (var rb in rbs)
        {
            rb.velocity = Vector3.zero;
            //rb.isKinematic = true;
        }
    }
}
