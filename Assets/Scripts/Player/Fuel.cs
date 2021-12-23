using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Fuel : PlayerField
{
    [SerializeField] private float _consumption;

    private Coroutine _currentCoroutine;

    public override event UnityAction<float> Changed;
    public event UnityAction Over;

    public void PrepareToConsume()
    {
        _currentCoroutine = StartCoroutine(Consume());
    }

    public void StopConsumption()
    {
        StopCoroutine(_currentCoroutine);
    }

    private IEnumerator Consume()
    {
        float deltaTime = 0.05f;
        var waiting = new WaitForSeconds(deltaTime);

        while (Value != 0)
        {
            Value = Mathf.MoveTowards(Value, 0, _consumption * deltaTime);
            Changed?.Invoke(Value);
            yield return waiting;
        }

        if (Value == 0)
        {
            Over?.Invoke();
        }
    }
}
