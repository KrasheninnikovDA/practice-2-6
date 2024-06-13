
using UnityEngine;

public class UnitView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void Move(Vector3 translation)
    {
        transform.Translate(translation);
    }

    public void SetDirection(Vector3 velosity)
    {
        _spriteRenderer.flipX = velosity.x < 0;
    }

    public void Rush(Vector3 rushVector)
    {
        Vector3 currentPosition = transform.position;
        currentPosition += rushVector;
        transform.position = currentPosition;
    }
}
