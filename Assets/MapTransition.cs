using System;
using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class MapTransition : MonoBehaviour
{
    [SerializeField]private PolygonCollider2D mapBoundary;
    private CinemachineConfiner2D confiner;
    [SerializeField]private Direction direction;
   

    private enum Direction {Up, Down, Left, Right}

    private void Awake()
    {
        confiner = FindFirstObjectByType<CinemachineConfiner2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            confiner.BoundingShape2D = mapBoundary;
            UpdatePlayerPosition(collision.gameObject);
        }
    }
    
    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;

        switch (direction)
        {
            case Direction.Up:
                newPos.y += 2;
                break;

            case Direction.Down:
                newPos.y -= 2;
                break;

            case Direction.Left:
                newPos.x -= 2;
                break;

            case Direction.Right:
                newPos.x += 2;
                break;
        }
        player.transform.position = newPos;
        
    }
    
}