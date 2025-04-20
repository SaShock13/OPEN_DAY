using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CarActivation : MonoBehaviour
{

    private CutScene _cutScene;
        
    [Inject]
    public void Construct(CutScene cutScene)
    {
        _cutScene = cutScene;
    }


    public void StartCutScene()
    {
        _cutScene.StartCutScene();
    }
}
