using System;
using System.Linq;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [Serializable]
    private class NodeArray
    {
        public const int ARRAY_LENGTH = 3;

        public Node[] Nodes;

        public NodeArray()
        {
            Nodes = new Node[ARRAY_LENGTH];
        }
    }
    
    [SerializeField]
    private NodeArray[] _rows;
    [SerializeField]
    private NodeArray[] _columns;
    [SerializeField]
    private NodeArray[] _diagonals;

    private void Start()
    {
        var allNodes = GetComponentsInChildren<Node>();

        for (int i = 1; i < allNodes.Length; i++)
        {
            allNodes[i].gameObject.name = String.Format("Node {0}", i);
        }

        SetupRows(allNodes);
        SetupColumns(allNodes);
        SetupDiagonals(allNodes);
    }

    private void Update()
    {
        //int count = 0;
        //Node.Icon comparisonIcon = Node.Icon.None;
        //foreach (Node node in (_nodes.SelectMany(x => x.Nodes).ToList()))
        //{
        //    if (comparisonIcon != node.VisibleIcon)
        //    {
        //        comparisonIcon = node.VisibleIcon;
        //        count = 0;
        //    }

        //    count++;
        //}

        //if (count == 3)
        //{
        //    Debug.Log("DING!");
        //}
        //else
        //{
        //    Debug.Log("FAIL");
        //}
    }

    private void SetupRows(Node[] nodeList)
    {
        _rows = GetInitializedNodeArray();

        for (int i = 0; i < 3; i++)
        {
            _rows[i].Nodes = nodeList
                .Skip(i * 3)
                .Take(3)
                .ToArray();
        }
    }

    private void SetupColumns(Node[] nodeList)
    {
        _columns = GetInitializedNodeArray();

        for (int i = 0; i < 3; i++)
        {
            var nodes = new Node[3];

            nodes[0] = nodeList[i + 0];
            nodes[1] = nodeList[i + 3];
            nodes[2] = nodeList[i + 6];

            _columns[i].Nodes = nodes;
        }
    }

    private void SetupDiagonals(Node[] nodeList)
    {
        _diagonals = GetInitializedNodeArray(2);

        var nodes = new Node[3];
        nodes[0] = nodeList[0];
        nodes[1] = nodeList[4];
        nodes[2] = nodeList[8];
        _diagonals[0].Nodes = nodes;
        
        nodes[0] = nodeList[2];
        nodes[1] = nodeList[4];
        nodes[2] = nodeList[6];
        _diagonals[1].Nodes = nodes;
    }

    private NodeArray[] GetInitializedNodeArray(int size = 3)
    {
        var arrayToReturn = new NodeArray[size];

        for (int i = 0; i < 3; i++)
        {
            arrayToReturn[i] = new NodeArray();
        }

        return arrayToReturn;
    }
}
