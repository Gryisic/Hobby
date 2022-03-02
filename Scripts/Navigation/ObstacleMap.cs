using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class ObstacleMap : MonoBehaviour
{
    private Tilemap _obstacles;

    public Tilemap GetObstacleMap()
    {
        if (_obstacles == null) _obstacles = GetComponent<Tilemap>();

        return _obstacles;
    }
}
