using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHealthBar : MonoBehaviour
{
    public Slider slider;

    // Used https://www.youtube.com/watch?v=BLfNP4Sc_iA
    // Can also add gradient

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
