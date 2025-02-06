using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    private UnityEngine.Color fxColor;

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _health = _player.GetComponent<PlayerHealth>();
        heartedImage  = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _health.onHealthChanged.AddListener(HealthChanged);
        _health.onDeath.AddListener(OnPlayerDeath);
        _health.onRebirth.AddListener(OnPlayerRebirth);

    }
    private void OnDestroy()
    {
        _health.onHealthChanged.RemoveListener(HealthChanged);
        _health.onDeath.RemoveListener(OnPlayerDeath);
        _health.onRebirth.RemoveListener(OnPlayerRebirth);
    }

    private void OnDisable()
    {
        //_health.onHealthChanged.RemoveListener(HealthChanged);
        //_health.onDeath.RemoveListener(OnPlayerDeath);
        //_health.onRebirth.RemoveListener(OnPlayerRebirth);
    }

    public void HealthChanged(float health,float maxHealth)
    {
        Debug.Log($"healthView reacted ");
        fxColor = heartedImage.color;
        fxColor.a = Mathf.Lerp(0,0.5f,1 - health/maxHealth);
        Debug.Log($"Health {health}");
        heartedImage.color = fxColor;        
    }

    public void OnPlayerDeath()
    {
        fxColor = heartedImage.color;
        fxColor.a = 1;
        Debug.Log($"Player HealthView On Death ");
        heartedImage.color = fxColor;
    }

    public void OnPlayerRebirth()
    {
        fxColor = heartedImage.color;
        fxColor.a = 0;
        Debug.Log($"Player HealthView On Rebirth ");
        heartedImage.color = fxColor;
    }
}
