using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CustomCard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardDescriptionText;
    [SerializeField] private CardDataScriptableObject cardData;

    [Header("Card Name")]
    [TextArea(1, 2)]
    [SerializeField] private string cardName = "Card Name";

    [Header("Card Description")]
    [TextArea(3, 5)]
    [SerializeField] private string cardDescription = "Description";

    public UnityEvent OnHovered;
    public UnityEvent OnHoverStopped;
    public UnityEvent OnSelected;

    public void OnHoverCard()
    {
        this.transform.localScale *= 1.1f;
        OnHovered?.Invoke();
    }

    public void OnStopHover()
    {
        this.transform.localScale /= 1.1f;
        OnHoverStopped?.Invoke();
    }

    public void OnSelectCard()
    {
        OnSelected?.Invoke();
    }

    public void LinkCardData(CardDataScriptableObject cardData)
    {
        this.cardData = cardData;
        cardName = cardData.cardName;
        cardDescription = cardData.cardDescription;

        cardNameText.text = cardName;
        cardDescriptionText.text = cardDescription;
    }
}


