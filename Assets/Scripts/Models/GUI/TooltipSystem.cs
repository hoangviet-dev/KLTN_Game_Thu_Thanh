using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    public Tooltip tooltip;
    public GameObject notEnoughMoneyPanel;

    void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "")
    {
        if (!string.IsNullOrEmpty(content) || !string.IsNullOrEmpty(header))
        {
            current.tooltip.gameObject.transform.position = Input.mousePosition;
            current.tooltip.SetText(content, header);
            current.tooltip.gameObject.SetActive(true);
        } else
        {
            Hide();
        }
    }

    public static void Hide()
    {
        if (current.tooltip.gameObject.activeSelf)
        {
            current.tooltip.gameObject.SetActive(false);
        }
    }

    public static void ShowNotEnoughMoneyPanel()
    {
        if (!current.notEnoughMoneyPanel.activeSelf)
        {
            current.notEnoughMoneyPanel.SetActive(true);
        }
    }
    public static void HideNotEnoughMoneyPanel()
    {
        if (current.notEnoughMoneyPanel.activeSelf)
        {
            current.notEnoughMoneyPanel.SetActive(false);
        }
    }
}