using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Watch : MonoBehaviour
{
    [SerializeField] TMP_Text livesText;


    /// <summary>
    /// ���� �������� ,��� ������ ������� ���� ���������� � ������� 
    /// </summary>
    
    public void UpdateLives(float health,float maxHealth)
    {
        livesText.text = ((int)health).ToString();
    }
}
