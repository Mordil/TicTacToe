using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    public enum Icon { None, O, X };

    public Icon VisibleIcon { get; private set; }

    [SerializeField]
    private Image XIcon;
    [SerializeField]
    private Image OIcon;

    private void Awake()
    {
        VisibleIcon = Icon.None;
    }

    public void OnClick()
    {
        // determine which icon to set based on who is the active player
        if (GameplayManager.Instance.ActivePlayerID == 1)
        {
            XIcon.gameObject.SetActive(true);
            VisibleIcon = Icon.X;
        }
        else
        {
            OIcon.gameObject.SetActive(true);
            VisibleIcon = Icon.O;
        }
        
        // ends the current player's turn
        GameplayManager.Instance.EndTurn();

        // disable this script component to no longer listen for OnClick calls
        this.enabled = false;
    }
}
