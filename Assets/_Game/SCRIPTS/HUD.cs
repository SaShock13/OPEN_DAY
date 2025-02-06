using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class HUD : MonoBehaviour
{
	private Player _player;
	private Canvas _canvas;
    [SerializeField] SteamVR_Action_Boolean pauseAction;
    [SerializeField] SteamVR_Action_Boolean restartAction;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject winPanel;
    private AudioSource winSound;
    [SerializeField] AudioClip winClip;
    private bool _pausePressed = false;
    private bool _restartPressed = false;
    private bool _paused = false;
    private bool _winned = false;



	[Inject]
	public void Construct(Player player)
	{
		_player = player;
	}

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
		_canvas.worldCamera = _player.GetComponentInChildren<Camera>();
        winSound = GetComponent<AudioSource>();
        winSound.clip = winClip;
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    private void Update()
    {
        _pausePressed = _player != null && _player.leftHand != null ? 
            pauseAction.GetStateDown(_player.leftHand.handType) || Input.GetKeyDown(KeyCode.P) : 
            Input.GetKeyDown(KeyCode.P);


        if (_winned)
        {
            _restartPressed = _player != null && _player.rightHand != null ?
            restartAction.GetState(_player.rightHand.handType) || Input.GetKeyDown(KeyCode.O) :
            Input.GetKeyDown(KeyCode.O);
            if (_restartPressed)
            {
                RestartGame();
            }
        }


        if (_pausePressed )
        {
            if (!_paused)
            {
                _paused = true;
                Time.timeScale = 0f;
                pausePanel.SetActive(true); 
            }
            else
            {
                _paused = false;
                Time.timeScale = 1f;
                _pausePressed = false;
                pausePanel.SetActive(false);
            }
        }
    }

    public void OnWinGame()
    {
        winPanel.SetActive(true);
        _winned = true;
        winSound.Play();
        Time.timeScale = 0f;
    }

    private void RestartGame()
    {

        Time.timeScale = 1f;
        Debug.Log($"retsrt {this}");
        winPanel.SetActive(false);
        winSound.Stop();
        _winned = false;
        _restartPressed = false;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
