using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class PathFinding 
{
    private const int STRAIGHT_NODE_COST = 10;
    private const int DIAGONAL_NODE_COST = 14;

    private static Tilemap _tilemap;

    public static List<PathFindingNode> BuildedPath(Vector2 from, Vector2 to)
    {
        if (_tilemap == null) SetObstacleMap();

        var reachableNodes = new List<PathFindingNode>();
        var exploredNodes = new List<PathFindingNode>();
        var grid = new PathFindingGrid();

        grid.Generate(from, to, _tilemap);

        var startingNode = grid.GetNodes[MathExtention.RoundedVector(from)];
        var endingNode = grid.GetNodes[MathExtention.RoundedVector(to)];
        var node = startingNode;

        node.SetCost(0, HeuristicCost(startingNode, endingNode));

        reachableNodes.Add(node);

        if (endingNode.Walkable)
        {
            while (reachableNodes.Count != 0)
            {
                node = NodeWithLowerCost(reachableNodes);

                if (node == endingNode) return BuildPath(endingNode);

                reachableNodes.Remove(node);
                exploredNodes.Add(node);

                var neighbours = grid.GetNeighbours(node).Except(exploredNodes).ToList();

                foreach (var neighbour in neighbours)
                {
                    int tentativeCost = node.GetGeneralCost + HeuristicCost(node, neighbour);

                    if (tentativeCost < neighbour.GetGeneralCost)
                    {
                        neighbour.SetParentNode(node);
                        neighbour.SetCost(tentativeCost, HeuristicCost(neighbour, endingNode));

                        if (reachableNodes.Contains(neighbour) == false && neighbour.Walkable)
                            reachableNodes.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    private static List<PathFindingNode> BuildPath(PathFindingNode endingNode)
    {
        List<PathFindingNode> path = new List<PathFindingNode>();

        path.Add(endingNode);

        PathFindingNode node = endingNode;

        while (node.GetParentNode != null)
        {
            path.Add(node.GetParentNode);
            node = node.GetParentNode;
        }

        path.Reverse();

        return path;
    }

    private static PathFindingNode NodeWithLowerCost(List<PathFindingNode> nodesToChoose)
    {
        var lowerCost = int.MaxValue;
        PathFindingNode bestNode = null;

        foreach (var node in nodesToChoose)
        {
            if (node.GetFinalCost < lowerCost)
            {
                bestNode = node;
                lowerCost = node.GetFinalCost;
            }
        }

        return bestNode;
    }

    private static int HeuristicCost(PathFindingNode from, PathFindingNode to)
    {
        var distanceX = MathExtention.RoundedValue(Mathf.Abs(from.GetPosition.x - to.GetPosition.x));
        var distanceY = MathExtention.RoundedValue(Mathf.Abs(from.GetPosition.y - to.GetPosition.y));
        var remainingDistance = Mathf.Abs(distanceX - distanceY);

        return DIAGONAL_NODE_COST * Mathf.Min(distanceX, distanceY) + STRAIGHT_NODE_COST * remainingDistance;
    }

    private static void SetObstacleMap()
    {
        _tilemap = Object.FindObjectOfType<ObstacleMap>().GetObstacleMap();
    }
}
