using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    [SerializeField] private Transform rangeIndicatorTransform;

    void Awake()
    {
        enabled = false;
    }

    public void SetRange(float range, Vector3 position)
    {
        transform.position = position;
        rangeIndicatorTransform.localScale = new Vector3(range * 2, range * 2, 1);
    }
}
