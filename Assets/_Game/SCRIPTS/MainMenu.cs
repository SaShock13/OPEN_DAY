using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject aboutPanel;

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();

        Debug.Log($"Выход из приложения  {this}");
    }

    public void OnAboutButtonClick()
    {

        Debug.Log($"About window {this}");
        aboutPanel.SetActive( true );
    }
}
