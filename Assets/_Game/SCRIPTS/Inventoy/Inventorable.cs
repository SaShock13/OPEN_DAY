using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Throwable))]
public class Inventorable : MonoBehaviour
{
    private bool isCanBePut = false;
    private Transform newParent = null;
    private Transform pastParent;
    private Rigidbody rigidbody;
    private bool isInInventory = false;
    private Cell activeCell;
    private Throwable throwable;

    private void Start()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
        throwable = GetComponent<Throwable>();
        throwable.onPickUp.AddListener(GetOutOfInventory);
        throwable.onDetachFromHand.AddListener(PutInInventory);
    }

    public void SetCanBePut( Cell cell )
    {
        isCanBePut = true;
        pastParent = transform.parent;
        newParent = cell.transform ;
        activeCell = cell;

        Debug.Log($"Active cell is :  {activeCell.name}");

    }
    public void SetCanNotBePut (Cell cell)
    {
        if (!isInInventory)
        {
            isCanBePut = false;
            activeCell = null;
            //Debug.Log($"Active cell is :  {activeCell.name}"); 
        }
    }


    public void PutInInventory()
    {
        if (!isCanBePut)
        {
            return;
        }
        if (!isInInventory)
        {
            Debug.Log($"In Inventory {this.name}");
            isInInventory = true;
            pastParent = transform.parent;//????
            transform.parent = newParent;
            rigidbody.isKinematic = true;
            transform.localScale = transform.localScale * 0.5f;
            activeCell.isCellBusy = true;
            Debug.Log($"active cell is  {activeCell!=null}");
            if (activeCell!=null)
            {
                activeCell.SetOpacity(0); 
            }
        }

    }

    public void GetOutOfInventory()
    {

        Debug.Log($"Out of inventory Method {this}");
        if (isInInventory)
        {
            transform.parent = null;
            rigidbody.isKinematic=false;
            transform.localScale = transform.localScale * 2f;
            isInInventory = false;
            activeCell.isCellBusy=false;

            //if (activeCell!=null)
            //{
            //    activeCell.SetTransparent(1); 
            //}
        }
    }



}
