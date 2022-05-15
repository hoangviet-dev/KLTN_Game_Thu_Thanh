using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEnemyInfoItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textSpeed;
    [SerializeField] private TextMeshProUGUI textHealth;
    [SerializeField] private TextMeshProUGUI textQuantity;

    public void SetTitle(string title)
    {
        textTitle.text = title;
    }

    public void SetSpeed(string speed)
    {
        textSpeed.text = speed;
    }

    public void SetHealth(string health)
    {
        textHealth.text = health;
    }

    public void SetQuantity(string quantity)
    {
        textQuantity.text = quantity;
    }
}
