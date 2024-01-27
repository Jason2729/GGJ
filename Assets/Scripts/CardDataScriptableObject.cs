using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObject", menuName = "Custom/Create New Custom Card Data")]
public class CardDataScriptableObject : ScriptableObject
{
    [Header("Player Attributes")]
    [Tooltip("modify the player's movement speed, default 1.0")]
    public float moveSpeedModifier = 1.0f;
    [Tooltip("modify the player's jump height, default 1.0")]
    public float jumpHeightModifier = 1.0f;
    [Tooltip("modify the player's body size, default 1.0")]
    public float sizeModifier = 1.0f;
    [Tooltip("modify the player's health, default 1.0")]
    public float healthModifier = 1.0f;
    [Tooltip("modify the player's gravity/fall speed, default 1.0")]
    public float gravityModifier = 1.0f;
    [Tooltip("modify the damage dealt by each player attack, default 1.0")]
    public float damageDealtModifier = 1.0f;
    public float damageTakenModifier = 1.0f;

    [Header("Weapon Parameters")]
    [Tooltip("weapon base damage, default 5")]
    public int weaponBaseDamage = 5;
    [Tooltip("weapon base range, default 2")]
    public float weaponBaseRange = 2;
    [Tooltip("modify the weapon range, default 1.0")]
    public float weaponSizeModifier = 1.0f;
    [Tooltip("modify the swing area of the weapon, default 1.0")]
    public float weaponArcModifier = 1.0f;
    [Tooltip("modify the cooldown between each attack, default 1.0")]
    public float attackCooldownModifier = 1.0f;

    [Header("Other Modifiers")]
    [Tooltip("balloons gained, default 0")]
    public int balloonsGained = 0;

    [Header("Card Name")]
    [TextArea(1,2)]
    public string cardName = "Card Name";

    [Header("Card Description")]
    [TextArea(3,5)]
    public string cardDescription = "Description";

}

public enum CardType
{
    PlayerAttributes,
    WeaponProperties,
    Other
}
