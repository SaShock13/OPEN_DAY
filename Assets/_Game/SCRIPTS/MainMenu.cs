using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    //TODO Часы показания жизней или время
    // TODO Молотов увеличить размер и сделать взрывным.
    // TODO ?? Предмет поднимается на уровень рук при подходе игрока!

    [SerializeField] private GameObject aboutPanel;
    [SerializeField] private GameObject mainPanel;

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);

        Debug.Log($"Старт приложения {this}");
    }

    public void OnQuitButtonClick()
    {
        //Application.Quit();

        Debug.Log($"Выход из приложения  {this}");
    }

    public void OnAboutButtonClick()
    {

        Debug.Log($"О игре  window {this}");
        aboutPanel.SetActive( true );
        mainPanel.SetActive(false);
    }

    public void OnBackButtonClick()
    {
        Debug.Log($"Возврат в меню {this}");
        aboutPanel.SetActive( false );
        mainPanel.SetActive(true);
    }

}
