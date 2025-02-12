using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public TMP_Text text;
    IEnumerator Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        while (true)
        {
            yield return new WaitForSeconds(0.2f); 
            text.text = ((int)(1 / Time.deltaTime)).ToString() + " fps";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
