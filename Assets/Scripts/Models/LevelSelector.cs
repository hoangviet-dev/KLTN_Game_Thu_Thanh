using Assets.Scripts.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private SceneFader sceneFader;
    [SerializeField] private Transform panelLevelButton;
    [SerializeField] private int levelQuantity;
    [SerializeField] private GameObject levelButtonPrefab;
    private List<Button> levelButtons;

    void Start()
    {
        int levelReached = DataCTLs.Instance.Level;
        levelButtons = new List<Button>();
        for (int i = 0; i < levelQuantity; i++)
        {
            GameObject objectLevelButton = Instantiate(levelButtonPrefab, panelLevelButton);
            Button button = objectLevelButton.GetComponent<Button>();
            if (button != null)
            {
                TextMeshProUGUI textMeshProUGUI = button.GetComponentInChildren<TextMeshProUGUI>();
                textMeshProUGUI.text = (i + 1).ToString();
                if (i + 1 > levelReached)
                {
                    button.interactable = false;
                }
                button.name = (i + 1).ToString();
                button.onClick.AddListener(() => Select(button.name));
                levelButtons.Add(button);
            }
        }
    }

    public void Select(string levelId)
    {
        BaseGameCTLs.Instance.MapId = levelId;
        BaseGameCTLs.Instance.LevelId = Int32.Parse(levelId);
        sceneFader.FadeTo(BaseGameCTLs.GAME_SCENE);
    }
}
