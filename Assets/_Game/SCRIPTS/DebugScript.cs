using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            print("Pressed LCtrl");
            if(Input.GetKeyDown(KeyCode.Z))
            {
                print("Pressed Z");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
