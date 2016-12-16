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
        var rand = Random.Range(0, 2);
        if (rand == 1)
        {
            XIcon.gameObject.SetActive(true);
            VisibleIcon = Icon.X;
        }
        else
        {
            OIcon.gameObject.SetActive(true);
            VisibleIcon = Icon.X;
        }

        this.enabled = false;
    }
}
