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
        image.color = new Color(0, 0, 0, 0);
    }

    public void OnClick()
    {
        if (manager == null) return;
        if (manager.drawpoint<=0) return;
        currentColor = manager.drawColor;
        image.color = currentColor;
        manager.drawpoint -= 1;
    }

    public void SetColor(Color c)
{
    currentColor = c;
    if (image == null) image = GetComponent<Image>();
    image.color = c;
}


    public Color GetColor()
    {
        return currentColor;
    }
}
