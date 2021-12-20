using UnityEngine;
using UnityEngine.Events;

public abstract class ObjectOfPool : MonoBehaviour
{
    protected static Player Player;

    public event UnityAction<ObjectOfPool> Disabled;

    public static void SetPlayer(Player player)
    {
        Player = player;
    }

    protected void OnDisable()
    {
        Disabled?.Invoke(this);
    }
}
