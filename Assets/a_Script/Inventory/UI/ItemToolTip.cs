using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemToolTip : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemInfoText;
    
    RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetupTooltip(ItemData_SO item)
    {
        itemNameText.text = item.itemName;
        itemInfoText.text = item.description;
    }

    private void OnEnable()
    {
        UpdatePosition();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        float width = corners[3].x - corners[0].x;
        float height = corners[1].y - corners[0].y;
        
        if(mousePos.y < height)
        {
            rectTransform.position = mousePos + Vector3.up * (height * 0.6f);
        }
        else if(Screen.width -mousePos.x > width)
        {
            rectTransform.position = mousePos + Vector3.right * (width * 0.6f);
        }
        else
        {
            rectTransform.position = mousePos + Vector3.left * (width * 0.6f);
        }
        
    }
}
