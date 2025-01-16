using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
    public SteamVR_Action_Boolean inventoryAction = SteamVR_Input.GetBooleanAction("Inventory");

    // todo сделать активным инвентарь только при зажатой кнопке / и Вообще с экшенами разобраться

    private void Update()
    {
        bool XPushed = inventoryAction.GetStateDown(SteamVR_Input_Sources.LeftHand);
        bool XRelesed = inventoryAction.GetStateUp(SteamVR_Input_Sources.LeftHand);
        if (XPushed)
        {
            Debug.Log($"X button Pressed {this}");
            inventoryCanvas.SetActive(true);
        }
        if (XRelesed)
        {
            Debug.Log($"X button Relesed {this}");
            inventoryCanvas.SetActive(false);
        }

    }
}
