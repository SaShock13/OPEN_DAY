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

    private ControllerHintsExample hintsController;

    public SteamVR_Action actionForHint;
    [SerializeField] private string hintText = "Нажмите X для открытия инвентаря,\nНажмите ещё раз , для его скрытия";
    [SerializeField] private SteamVR_Action_Boolean action = SteamVR_Input.GetBooleanAction("Inventory");
    private string textToShow;
    [SerializeField] private HintType hintType = HintType.Walk;


    // todo сделать текст подсказок константами и свич енумов 
    private const string grabHintText = "Нажмите и удерживайте курок, чтобы держать предмет. \n С некоторыми предметами можно взаимодействовать, \n если при удержании курка нажать другой курок";

    [SerializeField] private Hands hand;
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

        //todo проверить свич, либо ентер в самом тексте в редакторе, либо промежуточной присваивать переменной , а потом в UI
        switch (hintType)
        {
            case HintType.Walk:
                hintText = grabHintText;
                break;
            case HintType.Turn:
                hintText = grabHintText;
                break;
            case HintType.Grab:
                hintText = grabHintText;
                break;
            case HintType.Inventory:
                hintText = grabHintText;
                break;
            case HintType.Pause:
                hintText = grabHintText;
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
            //hintsController.ShowTextHints(Player.instance.hands[0]);

            Debug.Log($"Pressed Q {this}");
            ControllerButtonHints.ShowTextHint(hintHand, actionIn, hintText);
        }
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    hintsController.ShowTextHints(Player.instance.hands[1]);
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    hintsController.ShowButtonHints(Player.instance.hands[0]);
        //}
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    hintsController.ShowButtonHints(Player.instance.hands[1]);
        //}
        if (Input.GetKeyDown(KeyCode.Z))
        {
            hintsController.DisableHints();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //hintsController.ShowTextHints(Player.instance.hands[0]);
        //hintsController.ShowButtonHints(Player.instance.hands[1]);
        if (!isAlreadyShown && other.CompareTag("Player"))
        {
            if (hintCoroutine == null)
            {
                hintCoroutine = StartCoroutine(ShowHintCoroutine(minHintTime));  
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    TurnHintOff();
        //}
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
