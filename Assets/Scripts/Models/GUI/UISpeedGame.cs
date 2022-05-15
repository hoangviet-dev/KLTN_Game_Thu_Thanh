using Assets.Scripts.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISpeedGame : MonoBehaviour
{
    [SerializeField] private List<float> listSpeed;
    [SerializeField] private TextMeshProUGUI textView;
    [SerializeField] private int speedIndex = 0;

    private void Awake()
    {
        textView.text = string.Format("X{0}", listSpeed[speedIndex]);
        BaseGameCTLs.Instance.SpeedGame = listSpeed[speedIndex];
    }

    public void ChangeSpeedGame()
    {
        speedIndex = (speedIndex + 1) % listSpeed.Count;
        textView.text = string.Format("X{0}", listSpeed[speedIndex]);
        BaseGameCTLs.Instance.SpeedGame = listSpeed[speedIndex];
    }
}
