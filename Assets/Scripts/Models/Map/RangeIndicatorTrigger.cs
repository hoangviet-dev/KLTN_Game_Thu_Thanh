using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangeIndicatorTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float range;
    public Vector3 position;

    public void OnPointerEnter(PointerEventData eventData)
    {
        RangeIndicatorSystem.ShowReview(range, position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RangeIndicatorSystem.HideReview();
    }
}
