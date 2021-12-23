using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class Filler : MonoBehaviour
{
    [SerializeField] protected float Step;
    protected PlayerField TargetField;

    protected Coroutine CurrentCoroutine;
    protected Image Image;
    protected float MaxValue;

    protected virtual void Awake()
    {
        Image = GetComponent<Image>();
    }

    protected void OnEnable()
    {
        TargetField.Reseted += Reset;
        TargetField.MaxValueChanged += OnMaxValueChanged;
        TargetField.Changed += OnChanged;
    }

    protected void OnDisable()
    {
        TargetField.Reseted -= Reset;
        TargetField.MaxValueChanged -= OnMaxValueChanged;
        TargetField.Changed -= OnChanged;
    }

    protected void Reset()
    {
        if (CurrentCoroutine != null)
        {
            StopCoroutine(CurrentCoroutine);
        }

        Image.fillAmount = 1;
    }

    protected void OnChanged(float value)
    {
        float target = value / MaxValue;

        if (CurrentCoroutine != null)
        {
            StopCoroutine(CurrentCoroutine);
        }

        CurrentCoroutine = StartCoroutine(ChangeValue(target));
    }

    protected IEnumerator ChangeValue(float target)
    {
        var waiting = new WaitForSeconds(0.05f);

        while (Image.fillAmount != target)
        {
            Image.fillAmount = Mathf.MoveTowards(Image.fillAmount, target, Step);
            yield return waiting;
        }
    }

    protected void OnMaxValueChanged(float value)
    {
        MaxValue = value;
    }
}
