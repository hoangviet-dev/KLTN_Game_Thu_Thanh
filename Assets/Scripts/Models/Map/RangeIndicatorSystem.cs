using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicatorSystem : MonoBehaviour
{
    private static RangeIndicatorSystem current;
    [SerializeField] private RangeIndicator rangeIndicatorTarget;
    [SerializeField] private RangeIndicator rangeIndicatorReview;

    void Awake()
    {
        current = this;
    }

    public static void ShowTarget(float range, Vector3 position)
    {
        if (range > 0)
        {
            current.rangeIndicatorTarget.SetRange(range, position);
            current.rangeIndicatorTarget.gameObject.SetActive(true);
        } else
        {
            HideTarget();
        }
    }

    public static void HideTarget()
    {
        current.rangeIndicatorTarget.gameObject.SetActive(false);
    }

    public static void ShowReview(float range, Vector3 position)
    {
        if (range > 0)
        {
            current.rangeIndicatorReview.SetRange(range, position);
            current.rangeIndicatorReview.gameObject.SetActive(true);
        }
        else
        {
            HideReview();
        }
    }

    public static void HideReview()
    {
        current.rangeIndicatorReview.gameObject.SetActive(false);
    }
}
