using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerModifiers", menuName = "PlayerModifiers", order = 0)]
public class PlayerModifiers : ScriptableObject
{
    [SerializeField] private float moveSpeedModifier = 1f;
    [SerializeField] private float jumpHeight = 1f;

    public void ApplyCardEffect(CardDataScriptableObject cardData)
    {

    }

    public void RemoveCardEffect()
    {

    }

    public void ResetStat()
    {

    }
    public void ResetAllStats()
    {

    }
}
