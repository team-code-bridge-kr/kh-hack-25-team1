using UnityEngine;
using UnityEngine.UI;

public class PixelButton : MonoBehaviour
{
    private Image image;
    private Color currentColor = Color.white;
    private PixelGridManager manager;

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void Init(PixelGridManager mgr)
    {
        manager = mgr;
        image = GetComponent<Image>();
        image.color = Color.white;
    }

    public void OnClick()
    {
        if (manager == null) return;
        currentColor = manager.drawColor;
        image.color = currentColor;
    }

    public Color GetColor()
    {
        return currentColor;
    }
}
