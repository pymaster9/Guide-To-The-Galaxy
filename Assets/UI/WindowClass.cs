using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class WindowClass : MonoBehaviour, IPointerEnterHandler//, IPointerExitHandler
{
    public GameObject parent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            parent.transform.position = Input.mousePosition;
        }
    }
    public void Drag()
    {
        transform.position = Input.mousePosition;
    }
}
