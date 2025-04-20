using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Valve.VR.InteractionSystem;
using Zenject;

public class CutScene : MonoBehaviour
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private float cutSceneDuration = 2f;
    [SerializeField] private bool isForcedStart;
    [SerializeField] private Transform univerPlayerTransform;
    [SerializeField] private Transform homePlayerTransform;
    [SerializeField] private Vector3 carPositionOffset;

    private Vector3 targetPlayerPosition;
    private Player _player;
    private CarActivation _carActivation;
    private bool isPlayerAtHome = true;
    private CircularDrive doorCircular;

    [Inject]
    public void Construct(Player player,CarActivation carActivation)
    {
        _player = player;
        _carActivation = carActivation;
    }

    private void Start()
    {
        targetPlayerPosition = univerPlayerTransform.position;
        doorCircular = _carActivation.GetComponentInChildren<CircularDrive>();
    }

    private void Update()
    {
        if(isForcedStart)
        {
            isForcedStart = false;
            StartCutScene();
        }
    }
    public void StartCutScene()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }
    public void StartVideo()
    {
        StartCoroutine(StartVideoCoroutine());
    }
    private void StopCutScene()
    {
        fadeAnimator.SetTrigger("FadeOut");
        isForcedStart = false;
    }
    IEnumerator StartVideoCoroutine()
    {        
        targetPlayerPosition = isPlayerAtHome?univerPlayerTransform.position:homePlayerTransform.position;
        _player.GetComponent<CharacterController>().enabled = false;
        _player.transform.position = targetPlayerPosition;
        _player.GetComponent<CharacterController>().enabled = true;
        _carActivation.transform.position = targetPlayerPosition + carPositionOffset;
        isPlayerAtHome = !isPlayerAtHome;
        yield return new WaitForSeconds(cutSceneDuration);
        DoorToStart();
        StopCutScene();
    }

    private void DoorToStart()
    {
        var doorRotation = doorCircular.transform.rotation;
        doorRotation =  Quaternion.Euler( new Vector3( doorRotation.x,doorCircular.startAngle,doorRotation.z));
        doorCircular.transform.localRotation = doorRotation;
        doorCircular.outAngle = doorCircular.startAngle;
    }   
}
