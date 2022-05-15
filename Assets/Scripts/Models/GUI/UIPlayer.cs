using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHealth;
    [SerializeField] private TextMeshProUGUI textMoney;
    void Start()
    {
        
    }

    void Update()
    {

    }

    private void LateUpdate()
    {
        textHealth.text = BaseGameCTLs.Instance.Health.ToString();
        textMoney.text = BaseGameCTLs.Instance.Money.ToString();
    }
}
