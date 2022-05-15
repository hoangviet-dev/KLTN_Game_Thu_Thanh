using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string content;
    public string header;

    private bool showNotEnoughMoney;
    private bool isShowTrigger;

    void Awake()
    {
        isShowTrigger = false;
        showNotEnoughMoney = false;
    }

    public bool ShowNotEnoughMoney
    {
        get { return showNotEnoughMoney; }
        set { showNotEnoughMoney = value; ShowNotEnoughMoneyPanel(); }
    }

    private void ShowNotEnoughMoneyPanel()
    {
        if (isShowTrigger)
        {
            if (showNotEnoughMoney)
            {
                TooltipSystem.ShowNotEnoughMoneyPanel();
            }
            else
            {
                TooltipSystem.HideNotEnoughMoneyPanel();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isShowTrigger = true;
        TooltipSystem.Show(content, header);
        ShowNotEnoughMoneyPanel();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isShowTrigger = false;
        TooltipSystem.Hide();
        TooltipSystem.HideNotEnoughMoneyPanel();
    }
}
