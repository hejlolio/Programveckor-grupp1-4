using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float maxHeath)
    {
        healthBarSlider.maxValue = maxHeath;
        healthBarSlider.value = maxHeath;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        healthBarSlider.value = health;

        fill.color = gradient.Evaluate(healthBarSlider.normalizedValue);
    }
}
