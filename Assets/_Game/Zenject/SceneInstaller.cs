using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] Smartphone phone;
    [SerializeField] CutScene cutScene;
    [SerializeField] CarActivation carActivation;
    public override void InstallBindings()
    {
        Container.Bind<Smartphone>().FromInstance(phone).AsSingle().NonLazy();
        Container.Bind<CutScene>().FromInstance(cutScene).AsSingle();
        Container.Bind<CarActivation>().FromInstance(carActivation).AsSingle();
    }
}