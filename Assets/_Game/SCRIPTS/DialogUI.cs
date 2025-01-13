using System.Collections;
using System.Collections.Generic;
using Assets._Game.SCRIPTS;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Zenject;

public class DialogUI : MonoBehaviour
{
    private TMP_Text dialogMessage;
    private bool isShowing = false;
    private Canvas canvas;
    private Player _player;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }


    [SerializeField] private List<UIDialogMessage> messagesList = new List<UIDialogMessage>();

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        dialogMessage = GetComponentInChildren<TMP_Text>();
        dialogMessage.enabled = false;
        canvas.worldCamera = _player.GetComponentInChildren<Camera>();
        canvas.planeDistance = 0.8f;
    }

    private void Update()
    {
        if (messagesList.Count != 0)
        {

            if (!isShowing)
            {
                StartCoroutine(ShowMessageCoroutine(messagesList[0]));
            }
        }
        else
        {
            dialogMessage.enabled = false;
        }
    }

    public void AddMessageToDialog(UIDialogMessage message)
    {
        messagesList.Add(message);
    }

    //public void ShowDialog(UIDialogMessage message)
    //{
    //    messagesList.Add(message);
    //    if (!isShowing)
    //    {
    //        StartCoroutine(ShowMessageCoroutine(message));
    //    }

    //}



    /// <summary>
    /// Воспроизводит по очереди все сообщения из списка, если они есть. в update ?
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private IEnumerator ShowMessageCoroutine(UIDialogMessage message)
    {
        isShowing = true;
        dialogMessage.enabled = true;
        dialogMessage.text = message.message;
        yield return new WaitForSeconds(message.showingTime);
        messagesList.Remove(message);
        isShowing = false;
        Debug.Log($"Сообщений в очереди осталось {messagesList.Count}");
    }
}
