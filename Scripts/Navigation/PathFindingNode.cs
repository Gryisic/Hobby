using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFindingNode
{
    public int GetGeneralCost => _generalCost;
    public int GetHeuristicCost => _heuristicCost;
    public int GetFinalCost => _generalCost + _heuristicCost;

    public bool Walkable => _tile ? false : true;

    public Vector2 GetPosition => _position;

    public PathFindingNode GetParentNode => _parentNode;

    private int _generalCost;
    private int _heuristicCost;

    private Tile _tile;
    private Vector2 _position;

    private PathFindingNode _parentNode;

    public PathFindingNode(Tile tile, Vector2 position)
    {
        _tile = tile;
        _position = position;

        _generalCost = int.MaxValue;
    }

    public void SetCost(int generalCost, int heuristicCost)
    {
        _generalCost = generalCost;
        _heuristicCost = heuristicCost;
    }

    public void SetParentNode(PathFindingNode parent) => _parentNode = parent; 
}
