using UnityEngine;
using UnityEngine.UI;

public class BattleCell : MonoBehaviour
{
    public bool IsShip { get; set; }
    public int x, y;
    public Sprite secondSprite;

    private Button button;
    private Sprite defaultSprite;
    private Image image;
    private MiniGameField field;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCellClick);

        image = GetComponent<Image>();
        defaultSprite = image.sprite;

        field = GetComponentInParent<MiniGameField>();
    }

    private void OnCellClick()
    {
        field.OnCellClick(x, y);

        button.interactable = false;
        image.sprite = secondSprite;
    }

    public void SetDefault()
    {
        IsShip = false;
        image.sprite = defaultSprite;
        button.interactable = true;
    }
}
