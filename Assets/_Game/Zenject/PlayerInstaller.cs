using System.ComponentModel;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] GameObject playerPrefab;
    //[SerializeField] Player player;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromComponentInNewPrefab(playerPrefab).AsSingle().NonLazy();
        //Container.Bind<Player>().FromInstance(player).AsSingle().NonLazy();
    }

}