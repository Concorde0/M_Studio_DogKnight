using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestNameButton : MonoBehaviour
{
    public TextMeshProUGUI questNameText;
    public QuestData_SO currentData;
    public TextMeshProUGUI questContentText;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(UpdateQuestContent);
    }

    private void UpdateQuestContent()
    {
        questContentText.text = currentData.description;
        QuestUI.Instance.SetupRequireList(currentData);

        foreach (Transform item in QuestUI.Instance.rewardTransform)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in currentData.rewards)
        {
            QuestUI.Instance.SetupRewardItem(item.itemData,item.amount);
        }
    }

    public void SetupNameButton(QuestData_SO questData)
    {
        currentData = questData;
        if (questData.isCompleted)
        {
            questNameText.text = questData.questName + "(completed)";
        }
        else
        {
            questNameText.text = questData.questName;
        }
    }
}
