using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;
using Zenject;

public class PlayerHealthView : MonoBehaviour
{
    private Player _player;
    private PlayerHealth _health;
    private Image heartedImage;
    private Color fxColor;

    //[Inject]
    //public void Construct(Player player)
    //{
    //    _player = player;
    //}

    private void Awake()
    {
        //_health = _player.GetComponent<PlayerHealth>();
        heartedImage  = GetComponent<Image>();
    }

    //private void OnEnable()
    //{
    //    _health.onHealthChanged += HealthChanged();
    //}

    //private void OnDisable()
    //{

    //}

    public void HealthChanged(float health,float maxHealth)
    {

        Debug.Log($"healthView reacted ");
        fxColor = heartedImage.color;
        fxColor.a = Mathf.Lerp(0,0.5f,1 - health/maxHealth);


        //Debug.Log($"alpha {fxColor.a}");

        Debug.Log($"Health {health}");
        heartedImage.color = fxColor;
        
    }


}
