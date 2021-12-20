using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ObjectOfPool>(out ObjectOfPool objectOfPool))
        {
            objectOfPool.gameObject.SetActive(false);
        }
        else if (collision.TryGetComponent<Ground>(out Ground ground))
        {
            Destroy(ground.gameObject);
        }
    }
}
