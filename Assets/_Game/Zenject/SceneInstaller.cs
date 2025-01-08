using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private Smartphone phone;
    [SerializeField] private CutScene cutScene;
    [SerializeField] private CarActivation carActivation;
    [SerializeField] private DialogUI dialogUI;
    public override void InstallBindings()
    {
        Container.Bind<Smartphone>().FromInstance(phone).AsSingle().NonLazy();
        Container.Bind<CutScene>().FromInstance(cutScene).AsSingle();
        Container.Bind<CarActivation>().FromInstance(carActivation).AsSingle();
        Container.Bind<DialogUI>().FromInstance(dialogUI).AsSingle();
    }
}