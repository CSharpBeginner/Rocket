using UnityEngine;

public class SquareOfSpawn : MonoBehaviour
{
    [SerializeField] private Vector2 _size;

    public Vector2 LeftBottomCorner => new Vector2(transform.position.x - _size.x / 2, transform.position.y - _size.y / 2);
    public float Width => _size.x;
    public float Height => _size.y;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _size);
    }
}
