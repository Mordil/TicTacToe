using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    public bool IsGameOver { get; private set; }

    public int ActivePlayerID { get; private set; }
    public int WinningPlayerID { get; private set; }

    [SerializeField]
    private Text WinText;

    private void Awake()
    {
        Instance = this;
    }

    public void EndTurn()
    {
        ActivePlayerID = (ActivePlayerID == 1) ? 2 : 1;
    }

    public void SetPlayerHasWon(Node.Icon iconWon)
    {
        IsGameOver = true;
        WinningPlayerID = (iconWon == Node.Icon.X) ? 1 : 2;

        WinText.gameObject.SetActive(true);
        WinText.text = string.Format("Player {0} Wins!", WinningPlayerID);
    }
}
