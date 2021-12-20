using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Shower : MonoBehaviour
{
    protected Text Text;

    protected void Awake()
    {
        Text = GetComponent<Text>();
    }

    protected void Show(float value)
    {
        int convertedValue = (int)value;
        Show(convertedValue);
    }

    protected void Show(int value)
    {
        Text.text = value.ToString();
    }
}
