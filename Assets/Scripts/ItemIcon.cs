using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemIcon : MonoBehaviour, IPointerClickHandler
{
    public Owner_ owner;

    public BottomUi bottomUi;

    private void Start()
    {
        bottomUi = FindObjectOfType<BottomUi>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        bottomUi.LoadUI((Base_)owner);
    }
}
