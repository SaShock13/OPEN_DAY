using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryObj;
    public SteamVR_Action_Boolean inventoryAction = SteamVR_Input.GetBooleanAction("Inventory");

    // todo сделать активным инвентарь только при зажатой кнопке / и Вообще с экшенами разобраться

    private void Update()
    {
        bool XPushed = inventoryAction.GetStateDown(SteamVR_Input_Sources.LeftHand);
        bool XRelesed = inventoryAction.GetStateUp(SteamVR_Input_Sources.LeftHand);
        if (XPushed)
        {
            Debug.Log($"X button Pressed {this}");
            inventoryObj.SetActive(true);
        }
        if (XRelesed)
        {
            Debug.Log($"X button Relesed {this}");
            inventoryObj.SetActive(false);
        }

    }
}
