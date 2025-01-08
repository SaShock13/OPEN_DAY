using Assets._Game.SCRIPTS;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class CollectionSpace : MonoBehaviour
{
    private FlamableMixComponent flamableMixComponent;
    FlamableMixComponent[] flamableMixArray = new FlamableMixComponent[6] ;
    bool isQuestCompleted = false;
    private DialogUI _dialogUI;
    [SerializeField] UnityEvent nextPhaseEvent;

    [Inject]
    public void Construct(DialogUI dialogUI)
    {
        _dialogUI = dialogUI;
    }

    private void Start()
    {
        for (int i = 0; i < flamableMixArray.Length - 1; i++)
        {
            flamableMixArray[i] = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<FlamableMixComponent>(out flamableMixComponent))
        {

            Debug.Log($"Conponent {flamableMixComponent.id} is collected ");
            flamableMixArray[flamableMixComponent.id] = flamableMixComponent;
            if(IsCompleted())
            {
                isQuestCompleted = true;

                Debug.Log($"Quest completed");
                nextPhaseEvent?.Invoke();
                //EverythingIsOnFire();
            }
        }
    }

    private bool IsCompleted()
    {
        for(int i = 0; i < flamableMixArray.Length; i++)
        {
            if (flamableMixArray[i] == null)
            {
                Debug.Log($"Quest NOT Yet completed ");
                return false;

            }
        }
        return true;
    }

    private void EverythingIsOnFire()
    {
        _dialogUI.AddMessageToDialog(new UIDialogMessage("Вещества загораются, занимаются стены, потолок , мебель, и сгорает всё здание до тла. Это конец !!!", 50));
    }

    public void DestroyFlamableComponents()
    {
        for (int i = 0; i < flamableMixArray.Length; i++)
        {
            if (flamableMixArray[i]!= null)
            {
                Destroy(flamableMixArray[i].gameObject);
            }
        }
    }
}
