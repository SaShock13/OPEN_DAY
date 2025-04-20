using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject doubleHandTrigger;
    [SerializeField] GameObject kickTrigger;
    public event Action EndAttackEvent;

    private void Start()
    {
        doubleHandTrigger.SetActive(false);
        kickTrigger.SetActive(false);
    }
    public void DoubleHandAttackStart()
    {
        doubleHandTrigger.SetActive(true);
        StartCoroutine(TurnTriggerOff(doubleHandTrigger,1));
    }
    public void DoubleHandAttackStop()
    {
        doubleHandTrigger.SetActive(false);
    }
    public void KickAttackStart()
    {
        kickTrigger.SetActive(true);
        StartCoroutine(TurnTriggerOff(kickTrigger,0.5f));
    }
    public void KickAttackStop()
    {
        kickTrigger.SetActive(false);
    }

    public void AnimationFinished()
    {
        EndAttackEvent?.Invoke();
    }

    IEnumerator TurnTriggerOff(GameObject triggerObj,float time) // Выключение триггера На случай прерывания анимации
    {
        yield return new WaitForSeconds(time);
        triggerObj.SetActive(false);
    }

}
