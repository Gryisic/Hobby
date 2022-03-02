using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFindingGrid  
{
    private const int NODE_WIDTH_n_HEIGHT = 1;

    public Dictionary<Vector2, PathFindingNode> GetNodes => _nodes;

    private Tilemap _obstacles;

    private Dictionary<Vector2, PathFindingNode> _nodes;

    public void Generate(Vector2 from, Vector2 to, Tilemap obstacles)
    {
        _obstacles = obstacles;

        _nodes = new Dictionary<Vector2, PathFindingNode>();

        _nodes.Clear();

        var nodePosition = new Vector2();

        var directionModifierX = MathExtention.Direction(from.x - to.x) * -1;
        var directionModifierY = MathExtention.Direction(from.y - to.y) * -1;

        from = MathExtention.RoundedVector(from);
        to = MathExtention.RoundedVector(to);

        for (var x = from.x; x != to.x + directionModifierX; x += NODE_WIDTH_n_HEIGHT * directionModifierX)
        {
            for (var y = from.y; y != to.y + directionModifierY; y += NODE_WIDTH_n_HEIGHT * directionModifierY)
            {
                nodePosition.x = x;
                nodePosition.y = y;

                var node = GenerateNode(nodePosition);

                _nodes.Add(nodePosition, node);
            }
        }
    }

    public IEnumerable<PathFindingNode> GetNeighbours(PathFindingNode originNode)
    {
        Vector2 originPosition = originNode.GetPosition;

        yield return NeighbourNode(originPosition + new Vector2(NODE_WIDTH_n_HEIGHT, 0));
        yield return NeighbourNode(originPosition + new Vector2(NODE_WIDTH_n_HEIGHT, NODE_WIDTH_n_HEIGHT));
        yield return NeighbourNode(originPosition + new Vector2(0, NODE_WIDTH_n_HEIGHT));
        yield return NeighbourNode(originPosition + new Vector2(-NODE_WIDTH_n_HEIGHT, NODE_WIDTH_n_HEIGHT));
        yield return NeighbourNode(originPosition + new Vector2(-NODE_WIDTH_n_HEIGHT, 0));
        yield return NeighbourNode(originPosition + new Vector2(-NODE_WIDTH_n_HEIGHT, -NODE_WIDTH_n_HEIGHT));
        yield return NeighbourNode(originPosition + new Vector2(0, -NODE_WIDTH_n_HEIGHT));
        yield return NeighbourNode(originPosition + new Vector2(NODE_WIDTH_n_HEIGHT, -NODE_WIDTH_n_HEIGHT));
    }

    private PathFindingNode GenerateNode(Vector2 position) =>
        new PathFindingNode(_obstacles.GetTile<Tile>(_obstacles.WorldToCell(position)), position);

    private PathFindingNode NeighbourNode(Vector2 position)
    {
        if(_nodes.ContainsKey(position) == false)
            _nodes.Add(position, GenerateNode(position));

        return _nodes[position];
    }
}
