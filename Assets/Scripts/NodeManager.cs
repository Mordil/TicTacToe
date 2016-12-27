using System;
using System.Linq;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [Serializable]
    private class NodeArray
    {
        public const int ARRAY_LENGTH = 3;

        public Node.Icon MatchingIcon { get; private set; }
        
        public Node[] Nodes;

        public NodeArray()
        {
            Nodes = new Node[ARRAY_LENGTH];
        }

        public bool AllNodesMatch()
        {
            int count = 0;
            Node.Icon comparisonIcon = Node.Icon.None;

            foreach (Node node in Nodes)
            {
                if (comparisonIcon != node.VisibleIcon)
                {
                    comparisonIcon = node.VisibleIcon;
                    count = 0;
                }

                count++;
            }

            var allMatch = count == ARRAY_LENGTH;
            if (allMatch)
            {
                MatchingIcon = comparisonIcon;
            }

            return allMatch;
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
        // find all nodes in the scene
        var allNodes = GetComponentsInChildren<Node>();

        // set their names to match what we need in code for runtime validation
        for (int i = 1; i < allNodes.Length; i++)
        {
            allNodes[i].gameObject.name = String.Format("Node {0}", i);
        }

        // setup references that match a tic-tac-toe model
        SetupRows(allNodes);
        SetupColumns(allNodes);
        SetupDiagonals(allNodes);
    }

    private void Update()
    {
        if (GameplayManager.Instance.IsGameOver)
        {
            return;
        }

        NodeArray matchingArray;

        bool hasMatchingRow = CheckArrayForMatches(_rows, out matchingArray);
        if (!hasMatchingRow)
        {
            bool hasMatchingColumn = CheckArrayForMatches(_columns, out matchingArray);
            if (!hasMatchingColumn)
            {
                bool hasMatchingDiagonal = CheckArrayForMatches(_diagonals, out matchingArray);
                if (hasMatchingDiagonal)
                {
                    GameplayManager.Instance.SetPlayerHasWon(matchingArray.MatchingIcon);
                }
            }
            else
            {
                GameplayManager.Instance.SetPlayerHasWon(matchingArray.MatchingIcon);
            }
        }
        else
        {
            GameplayManager.Instance.SetPlayerHasWon(matchingArray.MatchingIcon);
        }
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

        nodes = new Node[3];
        nodes[0] = nodeList[2];
        nodes[1] = nodeList[4];
        nodes[2] = nodeList[6];
        _diagonals[1].Nodes = nodes;
    }

    private bool CheckArrayForMatches(NodeArray[] arrayToCheck, out NodeArray matchingArray)
    {
        matchingArray = null;

        foreach (NodeArray array in arrayToCheck)
        {
            if (array.AllNodesMatch() &&
                array.MatchingIcon != Node.Icon.None)
            {
                matchingArray = array;
                return true;
            }
        }

        return false;
    }

    private NodeArray[] GetInitializedNodeArray(int size = 3)
    {
        var arrayToReturn = new NodeArray[size];

        for (int i = 0; i < size; i++)
        {
            arrayToReturn[i] = new NodeArray();
        }

        return arrayToReturn;
    }
}
