using UnityEngine;

public class SpaceBackgroundTransition : Transition
{
    [SerializeField] private Fader _spaceBackground;

    protected override void Make()
    {
        _spaceBackground.SetStartAltitude(TransitionValue);
        _spaceBackground.gameObject.SetActive(true);
        _spaceBackground.enabled = true;
    }
}
