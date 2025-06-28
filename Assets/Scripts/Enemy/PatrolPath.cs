using System;
using UnityEngine;

public partial class PatrolPath : MonoBehaviour
{
    public Vector2 startPosition, endPosition;

    public Mover CreateMover(float speed = 1) => new Mover(this, speed);

    void Reset()
    {
        startPosition = Vector3.left;
        endPosition = Vector3.right;
    }
}
[Serializable]
public class PatrolPathDTO
{
    public Vector2 globalPosition;
    public Vector2 startPosition;
    public Vector2 endPosition;
}