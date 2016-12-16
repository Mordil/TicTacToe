using System;
using System.Linq;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    private const int ROW_LENGTH = 3;

    [Serializable]
    private class NodeArray
    {
        public Node[] SubArray;

        public NodeArray()
        {
            SubArray = new Node[ROW_LENGTH];
        }
    }

    [SerializeField]
    private NodeArray[] _nodes;

    private void Start()
    {
        if (_nodes.Length <= 0)
        {
            _nodes = new NodeArray[ROW_LENGTH];
            for (int i = 0; i < ROW_LENGTH; i++)
            {
                _nodes[i] = new NodeArray();
            }

            var allNodes = GetComponentsInChildren<Node>();

            for (int i = 1; i < allNodes.Length; i++)
            {
                allNodes[i].gameObject.name = String.Format("Node {0}", i);
            }

            for (int i = 0; i < 3; i++)
            {
                var skippedNodes = allNodes.Skip(i * 3).ToArray();
                var takenNodes = skippedNodes.Take(3).ToArray();
                _nodes[i].SubArray = takenNodes;
            }
        }
    }

    private void Update()
    {
        int count = 0;
        Node.Icon comparisonIcon = Node.Icon.None;
        foreach (Node node in (_nodes.SelectMany(x => x.SubArray).ToList()))
        {
            if (comparisonIcon != node.VisibleIcon)
            {
                comparisonIcon = node.VisibleIcon;
                count = 0;
            }

            count++;
        }

        if (count == 3)
        {
            Debug.Log("DING!");
        }
        else
        {
            Debug.Log("FAIL");
        }
    }
}
