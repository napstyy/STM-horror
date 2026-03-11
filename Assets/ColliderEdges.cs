using UnityEngine;

public class ColliderEdges : MonoBehaviour
{
    void Awake()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        Vector2 size = box.size;
        Vector2 offset = box.offset;

        Vector2[] points = new Vector2[5];

        points[0] = offset + new Vector2(-size.x / 2, -size.y / 2); // bottom left
        points[1] = offset + new Vector2(size.x / 2, -size.y / 2);  // bottom right
        points[2] = offset + new Vector2(size.x / 2, size.y / 2);   // top right
        points[3] = offset + new Vector2(-size.x / 2, size.y / 2);  // top left
        points[4] = points[0]; // close the loop

        EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
        edge.points = points;

        Destroy(box);
    }
}