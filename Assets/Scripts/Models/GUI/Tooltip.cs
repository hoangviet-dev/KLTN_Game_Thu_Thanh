using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private RectTransform canvasRectTransform;
    private RectTransform rectTransform;

    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        if (anchoredPosition.x + rectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - rectTransform.rect.width;
        }
        if (anchoredPosition.y + rectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - rectTransform.rect.height;
        }

        anchoredPosition.x = Math.Max(0, anchoredPosition.x);
        anchoredPosition.y = Math.Max(0, anchoredPosition.y);
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public void SetText(string content, string header = "")
    {
        headerField.gameObject.SetActive(!string.IsNullOrEmpty(header));
        headerField.text = header;
        contentField.text = content;
    }


}