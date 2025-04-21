using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Чит коды для игрока
/// </summary>
public class PlayerCheats : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean firstAction ; // Первая кнопка в комбинации
    [SerializeField] private SteamVR_Action_Boolean secondAction; // Вторая кнопка в комбинации
    private bool firstbuttonActive = false;
    private bool secondbuttonActive = false;
    [SerializeField] private GameObject gunObject;

    private void Start()
    {
        firstAction.onState += FirstAction_onState;
        secondAction.onStateDown += SecondAction_onStateDown;
    }

    private void SecondAction_onStateDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {

        Debug.Log($"Вторая кнопка из комбинации нажата{this}");

    }

    private void FirstAction_onState(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        secondbuttonActive = secondAction.GetStateDown(SteamVR_Input_Sources.RightHand);

        //Debug.Log($"FIRSTACTION UPDATE {this}");
        if (secondbuttonActive)
        {

            Debug.Log($"Cheat is activated by controllers{this}");
            ActivateCheat();
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) // Активация кода через клавиатуру Shift + C
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log($"Cheat is activated by keyboard{this}");
                ActivateCheat();
            }
        }
        
    }

    private void ActivateCheat()
    {
        gunObject.SetActive(true);
    }
}
