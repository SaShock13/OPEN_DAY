using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    private Inventorable objToPut;
    [SerializeField] private Image cellImage;
    [SerializeField] private Color inActiveCellColor;
    [SerializeField] private Color activeCellColor;
    public bool isCellBusy = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCellBusy)
        {
            if (other.transform.parent.TryGetComponent<Inventorable>(out objToPut))
            {
                Debug.Log($"Can be Put in inventory {objToPut.name}");
                objToPut.SetCanBePut(this);
                cellImage.color = activeCellColor;
                //objToPut.transform.parent = transform;//todo ???? Здесь это делать ???
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isCellBusy)
        {
            if (other.transform.parent.TryGetComponent<Inventorable>(out objToPut))
            {
                Debug.Log($"CanNOT be put in inventory {objToPut.name}");
                objToPut.SetCanNotBePut(this);
                cellImage.color = inActiveCellColor;
            } 
        }
    }
    public void SetOpacity(float opacity)
    {

        Debug.Log($"Set Opacity {opacity}");
        cellImage.color = new Color(cellImage.color.r, cellImage.color.g, cellImage.color.b, opacity);
    }
}
