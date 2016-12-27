using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public int ActivePlayerID { get; private set; }

    public static GameplayManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void EndTurn()
    {
        ActivePlayerID = (ActivePlayerID == 1) ? 2 : 1;
    }
}
