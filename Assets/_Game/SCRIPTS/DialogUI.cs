using System.Collections;
using System.Collections.Generic;
using Assets._Game.SCRIPTS;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    private TMP_Text dialogMessage;
    private bool isShowing = false;
    [SerializeField] private List<UIDialogMessage> messagesList = new List<UIDialogMessage>();

    private void Start()
    {
        dialogMessage = GetComponentInChildren<TMP_Text>();
        dialogMessage.enabled = false;
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
