using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;

    public void UpdateHealthBar(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }
}