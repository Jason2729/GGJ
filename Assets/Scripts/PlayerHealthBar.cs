using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void UpdateHealthBar(float newHealthPercent)
    {
        healthBar.value = newHealthPercent;
    }
}
