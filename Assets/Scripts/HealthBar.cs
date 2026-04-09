using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;

    [Header("Color Settings")]
    [SerializeField] private Color highHealthColor = Color.green;
    [SerializeField] private Color midHealthColor = Color.yellow;
    [SerializeField] private Color lowHealthColor = Color.red;

    [Header("Thresholds")]
    [SerializeField] private float lowThreshold = 0.25f;
    [SerializeField] private float midThreshold = 0.60f;

    [Header("Smooth Bar")]
    [SerializeField] private bool smoothBar = true;
    [SerializeField] private float smoothSpeed = 5f;
    private float targetFill;

    private PlayerHealth trackedPlayer;

    void Awake()
    {
        if (slider == null)
            slider = GetComponent<Slider>();
    }

    public void Initialize(PlayerHealth playerHealth)
    {
        if (trackedPlayer != null)
            trackedPlayer.onHealthChanged.RemoveListener(OnHealthChanged);

        trackedPlayer = playerHealth;
        trackedPlayer.onHealthChanged.AddListener(OnHealthChanged);

        slider.maxValue = playerHealth.GetMaxHealth();
        slider.value    = playerHealth.GetCurrentHealth();
        targetFill      = slider.value;

        UpdateColor();
    }

    private void OnHealthChanged(int newHealth)
    {
        targetFill = newHealth;
        if (!smoothBar)
            slider.value = newHealth;
        UpdateColor();
    }

    void Update()
    {
        if (smoothBar && !Mathf.Approximately(slider.value, targetFill))
            slider.value = Mathf.Lerp(slider.value, targetFill, Time.deltaTime * smoothSpeed);
    }

    private void UpdateColor()
    {
        if (fillImage == null) return;
        float percent = targetFill / slider.maxValue;
        if (percent <= lowThreshold)
            fillImage.color = lowHealthColor;
        else if (percent <= midThreshold)
            fillImage.color = midHealthColor;
        else
            fillImage.color = highHealthColor;
    }

    void OnDestroy()
    {
        if (trackedPlayer != null)
            trackedPlayer.onHealthChanged.RemoveListener(OnHealthChanged);
    }
}