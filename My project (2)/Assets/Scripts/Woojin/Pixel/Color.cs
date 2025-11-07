using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public Slider rSlider, gSlider, bSlider;
    public Image previewImage;
    public PixelGridManager manager;

    void Start()
    {
        rSlider.onValueChanged.AddListener(UpdateColor);
        gSlider.onValueChanged.AddListener(UpdateColor);
        bSlider.onValueChanged.AddListener(UpdateColor);
        UpdateColor(0);
    }

    void UpdateColor(float _)
    {
        Color newColor = new Color(rSlider.value, gSlider.value, bSlider.value);
        previewImage.color = newColor;
        manager.drawColor = newColor;
    }
}
