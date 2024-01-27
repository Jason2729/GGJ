using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomCard : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardDescriptionText;

    [Header("Player Attributes")]
    [SerializeField] private float moveSpeedModifier = 1.0f;
    [SerializeField] private float jumpHeightModifier = 1.0f;
    [SerializeField] private float sizeModifier = 1.0f;
    [SerializeField] private float healthModifier = 1.0f;
    [SerializeField] private float gravityModifier = 1.0f;
    [SerializeField] private float damageDealtModifier = 1.0f;
    [SerializeField] private float damageTakenModifier = 1.0f;

    [Header("Weapon Parameters")]
    [SerializeField] private int weaponBaseDamage = 5;
    [SerializeField] private float weaponBaseRange = 2;
    [SerializeField] private float weaponSizeModifier = 1.0f;
    [SerializeField] private float weaponArcModifier = 1.0f;
    [SerializeField] private float attackCooldownModifier = 1.0f;

    [Header("Other Modifiers")]
    [SerializeField] private int balloonsGained = 0;

    [Header("Card Name")]
    [TextArea(1, 2)]
    [SerializeField] private string cardName = "Card Name";

    [Header("Card Description")]
    [TextArea(3, 5)]
    [SerializeField] private string cardDescription = "Description";

    public void OnHoverCard()
    {
        this.transform.localScale *= 1.1f;
    }

    public void OnResetCard()
    {
        this.transform.localScale /= 1.1f;
    }

    public void OnSelectCard()
    {

    }

    public void FillOutCard(CardDataScriptableObject cardData)
    {
        moveSpeedModifier = cardData.moveSpeedModifier;
        jumpHeightModifier = cardData.jumpHeightModifier;
        sizeModifier = cardData.sizeModifier;
        healthModifier = cardData.healthModifier;
        gravityModifier = cardData.gravityModifier;
        damageDealtModifier = cardData.damageDealtModifier;
        damageTakenModifier = cardData.damageTakenModifier;

        weaponBaseDamage = cardData.weaponBaseDamage;
        weaponBaseRange = cardData.weaponBaseRange;
        weaponSizeModifier = cardData.weaponSizeModifier;
        weaponArcModifier = cardData.weaponArcModifier;
        attackCooldownModifier = cardData.attackCooldownModifier;

        balloonsGained = cardData.balloonsGained;

        cardName = cardData.cardName;
        cardDescription = cardData.cardDescription;

        cardNameText.text = cardName;
        cardDescriptionText.text = cardDescription;
    }
}


