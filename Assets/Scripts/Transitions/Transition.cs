using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] protected float TransitionValue;
    [SerializeField] protected Transition NextTransition;

    protected abstract void Make();

    public Transition Transit(float value)
    {
        if (value > TransitionValue)
        {
            Make();
            return NextTransition;
        }

        return this;
    }
}
