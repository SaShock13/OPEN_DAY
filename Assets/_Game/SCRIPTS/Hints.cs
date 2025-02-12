using System.Collections;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Valve.VR.InteractionSystem.Sample;
using Hand = Valve.VR.InteractionSystem.Hand;


// todo Сделать немедленное выключение подсказки 
public class Hints : MonoBehaviour
{
    enum HintType
    {
        Walk,
        Turn,
        Grab,
        Inventory,
        Pause
    }
    enum Hands
    {
        Left,
        Right
    }

    [SerializeField] private SteamVR_Action_Boolean action = SteamVR_Input.GetBooleanAction("Inventory");
    [SerializeField] private HintType hintType = HintType.Walk;
    [SerializeField] private Hands hand;
    public SteamVR_Action actionForHint;

    private ControllerHintsExample hintsController;
    private string hintText;
    private string textToShow;

    private const string grabHintText = "Нажмите и удерживайте курок, чтобы держать предмет. \n С некоторыми предметами можно взаимодействовать, \n если при удержании курка нажать другой курок";
    private const string turnHintText = "На правом контроллере джойстик в сторону -  \n Поворот влево - вправо";
    private const string inventoryHintText = "Нажмите X для инвентаря";
    private const string pauseHintText = "Нажмите Y для паузы";
    private const string walkHintText = "На левом контроллере джойстик - перемещаться";

    private Hand hintHand;
    private SteamVR_Input_Sources input_Source;
    private float minHintTime = 1f;
    private bool skipHint = false;
    private Coroutine hintCoroutine;
    private ISteamVR_Action_In actionIn ;
    private bool isAlreadyShown = false; // Чтобы подсказка показывалась только один раз

    private void Start()
    {
        if (hand == Hands.Left) // устанавливает руку для отображения подсказки
        {
            hintHand = Player.instance.hands[0];
            input_Source = SteamVR_Input_Sources.LeftHand;
        }
        else
        {
            hintHand = Player.instance.hands[1];
            input_Source = SteamVR_Input_Sources.RightHand;
        } 
        actionIn = action;
        hintsController = GetComponent<ControllerHintsExample>();
        textToShow = hintText.ToString();
                       
        switch (hintType)
        {
            case HintType.Walk:
                hintText = walkHintText;
                break;
            case HintType.Turn:
                hintText = turnHintText;
                break;
            case HintType.Grab:
                hintText = grabHintText;
                break;
            case HintType.Inventory:
                hintText = inventoryHintText;
                break;
            case HintType.Pause:
                hintText = pauseHintText;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        bool isButtonPressed = action.GetStateDown(input_Source);

        if (isButtonPressed)
        {
            skipHint = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log($"Pressed Q {this}");
            ControllerButtonHints.ShowTextHint(hintHand, actionIn, hintText);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            hintsController.DisableHints();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAlreadyShown && other.CompareTag("Player"))
        {
            if (hintCoroutine == null)
            {
                hintCoroutine = StartCoroutine(ShowHintCoroutine(minHintTime));  
            }
        }        
    }

    private void TurnHintOff()
    {
        hintCoroutine = null;
        hintsController.DisableHints();
        ControllerButtonHints.HideTextHint(hintHand, actionIn);
    }

    IEnumerator ShowHintCoroutine(float time)
    {
        skipHint = false;
        isAlreadyShown = true;
        while (!skipHint)
        {
            ControllerButtonHints.ShowTextHint(hintHand, actionIn, hintText);
            yield return new WaitForSeconds(time);
        }
        TurnHintOff();
    }
}
