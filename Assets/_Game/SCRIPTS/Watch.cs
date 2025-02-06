using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Zenject;

public class Watch : MonoBehaviour
{

    // todo на rendermodel не обновляются показания
    [SerializeField] TMP_Text livesText;

    private Player _player;
    private PlayerHealth playerHealth;


    [Inject]
    public void Construct(Player player)
    {
        //_player = player;
        //playerHealth = _player.GetComponent<PlayerHealth>();
        //playerHealth.onHealthChanged.AddListener(UpdateLives);

        Debug.Log($"Inject to Watch Func OK {this}");
    }

    private void OnEnable()
    {
        //playerHealth.onHealthChanged.AddListener(UpdateLives);
    }


    private void Awake()
    {
        //_player = GetComponentInParent<Player>(true);

        //playerHealth = _player.GetComponent<PlayerHealth>();

        

        Debug.Log($"watch awake with {playerHealth} !!!!");

    }

    private IEnumerator Start()
    {
        while (true)
        {
            UpdateTime();
            yield return new WaitForSeconds(1); 
        }
    }

    /// <summary>
    /// Åñòü ñîìíåíèÿ ,÷òî ñêðèïò ïðåôàáà ðóêè ïðèâÿæåòñÿ ê ñîáûòèþ 
    /// </summary>

    public void UpdateLives(float health,float maxHealth)
    {
        //livesText.text = ((int)health).ToString();
        

        //Debug.Log($"Update watch Health {health}");
    }

    private void UpdateTime()
    {
        livesText.text = DateTime.Now.ToString("T");
    }

    private void OnDisable()
    {
        //playerHealth.onHealthChanged.RemoveListener(UpdateLives);
    }
}
