using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;

    [SerializeField] private TextMeshProUGUI hpSliderText;

    [SerializeField] private int sameMessageCount = 0;

    [SerializeField] private string lastMessage;

    [SerializeField] private bool isMessageHistoryOpen = false;

    [SerializeField] private GameObject messageHistory;

    [SerializeField] private GameObject messageHistoryContent;

    [SerializeField] private GameObject lastFiveMessagesContent;

    public bool IsMessageHistoryOpen { get => isMessageHistoryOpen; }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetHealthMax(int maxHp)
    {
        hpSlider.maxValue = maxHp;
    }

    public void SetHealth(int hp, int maxHp)
    {
        hpSlider.value = hp;
        hpSliderText.text = $"HP: {hp}/{maxHp}";
    }

    public void ToggleMessageHistory()
    {
        messageHistory.SetActive(!messageHistory.activeSelf);
        isMessageHistoryOpen = messageHistory.activeSelf;
    }
}
