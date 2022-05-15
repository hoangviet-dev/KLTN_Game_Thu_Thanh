using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINotification : MonoBehaviour
{
    private static UINotification instance;

    [SerializeField] private TextMeshProUGUI textCountDown;
    [SerializeField] private TextMeshProUGUI textTitle;
    [SerializeField] private TextMeshProUGUI textBoss;

    private int timedown;
    public static int TimeDown
    {
        get { return instance.timedown; }
        set { instance.timedown = value; instance.textCountDown.text = value.ToString(); }
    }

    private string title;

    public static string Title
    {
        get { return instance.title; }
        set { instance.title = value; instance.textTitle.text = value.ToString();}
    }

    private bool isBoss;
    public static bool IsBoss
    {
        get { return instance.isBoss; }
        set { instance.isBoss = value; instance.textBoss.gameObject.SetActive(value); }
    }

    private void Awake()
    {
        instance = this;
        textCountDown.text = textTitle.text = "";
        textBoss.gameObject.SetActive(false);
    }
}
