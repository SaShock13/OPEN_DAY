using System;
using System.Collections;
using System.Collections.Generic;
using Assets._Game.SCRIPTS;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;
using Zenject;

public class NPC_Doc : MonoBehaviour
{
    [SerializeField] UnityEvent makeFireWaterEvent;
    
    private float talkTimeInSeconds = 15;
    [SerializeField] private GameObject fireWater;
    [SerializeField] private int npcTalkPhase = 1;
    [SerializeField] private AudioClip talkPhase1;
    [SerializeField] private AudioClip talkPhase2;
    [SerializeField] private AudioClip talkPhase3;

    private Animator animator;
    private bool isTalking = false;
    private AudioSource talkSound;

    private DialogUI _dialogUI;

    [Inject]
    public void Construct(DialogUI dialogUI)
    {
        _dialogUI = dialogUI;
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        talkSound = GetComponent<AudioSource>();
        _dialogUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartTalk(); 
        }
    }
        
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Only for debuging
        {
            StartTalk();
        }
    }

    private void StartTalk()
    {
        if (!isTalking)
        {
            StartCoroutine("TalkCoroutine");
        }        
    }

    IEnumerator TalkCoroutine()
    {
        _dialogUI.gameObject.SetActive (true);
        isTalking = true;

        transform.rotation = Quaternion.LookRotation(Player.instance.transform.position - transform.position);
        Debug.Log($"Conversation with  {gameObject.name}");
        animator.SetBool("talking", isTalking);

        switch (npcTalkPhase)
        {
            case 1:
            TalkPhaseOne();
                break;
            case 2:
                TalkPhaseTwo();
                break;
            case 3:
                TalkPhaseThree();
                break;
            default:
                break;
        }

        talkSound.Play();

        yield return new WaitForSeconds(talkTimeInSeconds);
        isTalking = false;
        animator.SetBool("talking", isTalking);
        talkSound.Stop();
        _dialogUI.gameObject.SetActive(false);
        Debug.Log($"Conversation with  {gameObject.name} finished!!!");
    }


    // todo текст после первой фазы остается третей части 1 фазы постоянно , а голос ok
    // todo Доделать диалог после забора готовой жижи
    private void TalkPhaseOne()
    {
        talkSound.clip = talkPhase1;
        talkTimeInSeconds = talkPhase1.length;
        _dialogUI.AddMessageToDialog(new UIDialogMessage("В одной из комнат я видел огромное существо, я пытался причинить ему вред - но всё оказалось бесполезно. Единственный вариант - это сжечь его. ", talkTimeInSeconds / 3));
        _dialogUI.AddMessageToDialog(new UIDialogMessage("Если ты принесешь мне 5 канистр с горючими веществами и один баллон с кислородом, я сделаю зажигательную смесь , способную уничтожить всё, что угодно!!!", talkTimeInSeconds / 3));
        _dialogUI.AddMessageToDialog(new UIDialogMessage("Положи все на стол - и через пару минут заберешь готовую смертельную жижу!!!", talkTimeInSeconds / 3));
    }

    private void TalkPhaseTwo()
    {
        talkSound.clip = talkPhase2;
        talkTimeInSeconds = talkPhase2.length;
        _dialogUI.AddMessageToDialog(new UIDialogMessage("Отлично!! Дай мне пару минут , и я сделаю смертельную жижу!!!", talkTimeInSeconds ));    
        StartCoroutine(MakeFireWater());
    }

    private void TalkPhaseThree()
    {
        talkSound.clip = talkPhase3;
        talkTimeInSeconds = talkPhase3.length;
        _dialogUI.AddMessageToDialog(new UIDialogMessage("Готово! Можешь забрать на столе. Сожги этого мерзкого Жирдяя и сможешь спасти свою принцессу !!!", talkTimeInSeconds));
    }

    IEnumerator MakeFireWater()
    {
        yield return new WaitForSeconds(10f);
        makeFireWaterEvent.Invoke();
        fireWater.SetActive(true);
        NextTalkPhase();
    }

    public void NextTalkPhase()
    {
        npcTalkPhase++;
        if (npcTalkPhase > 3) npcTalkPhase = 3;
    }

}
