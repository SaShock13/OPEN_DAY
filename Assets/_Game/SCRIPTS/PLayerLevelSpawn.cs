using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;
using Zenject;

public class PLayerLevelSpawn : MonoBehaviour
{
    Player _player;    

    [Inject]
    public void Construct(Player player)
    {
        var _playerrr = player;
        _player = player;
    }
    
    private void Awake()
    {
        _player.transform.position = transform.position;

    }

    private void Start()
    {
        _player.GetComponentInChildren<SteamVR_LaserPointer>().pointer.GetComponent<Renderer>().enabled = false;
        
    }

}
