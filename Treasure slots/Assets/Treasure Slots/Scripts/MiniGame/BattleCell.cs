using UnityEngine;
using UnityEngine.UI;

public class BattleCell : MonoBehaviour
{
    public bool IsShip { get; set; }
    public int x, y;
    public Sprite secondSprite;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CellClicked);
    }

    private void CellClicked()
    {
        MiniGameField field = GetComponentInParent<MiniGameField>();

        field.CellClicked(x, y);

        button.interactable = false;
        GetComponent<Image>().sprite = secondSprite;
    }
}
