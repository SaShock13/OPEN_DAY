using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSync : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private Transform head;
    private Vector3 newPosition;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        newPosition = Vector3.zero;
    }

    private void Update()
    {
        newPosition.x = head.position.x;
        newPosition.z = head.position.z;
        newPosition.y = characterController.transform.position.y;
        characterController.transform.position = newPosition;
    }
}
