using UnityEngine;
using UnityEngine.Rendering.Universal;
using Valve.VR.InteractionSystem;
using Valve.VR;
using UnityEngine.Rendering;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Vector2 leftJoystickInput;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float vignetteCoeff = 0.5f;
    Vector3 lookDirection, inputVector3;
    CharacterController characterController;



    private Volume _volume;
    private Vignette _vignette;

    private void Awake()
    {
        _volume = FindObjectOfType<Volume>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {


        inputVector3 = new Vector3(leftJoystickInput.axis.x, 0, leftJoystickInput.axis.y);
        lookDirection = Player.instance.hmdTransform.TransformDirection(inputVector3);
        var moveDirection = moveSpeed * Time.deltaTime * (Vector3.ProjectOnPlane(lookDirection, Vector3.up)).normalized;
        characterController.Move(moveDirection - (new Vector3(0, 9.8f, 0) * Time.deltaTime));
        
        //_volume.profile.TryGet(out _vignette);        
        //_vignette.intensity.value = inputVector3.normalized.magnitude * vignetteCoeff;
    }
}
