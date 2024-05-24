using UnityEngine;

public class MiniGameField : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int fieldSize;
    [SerializeField] private Sprite shipSpite;
    [SerializeField] private Sprite seaMineSpite;
    [SerializeField] private MiniGameCircle gameCircle;
    [SerializeField] private int chance = 90;
    private float cellSize;
    private BattleCell[,] cells;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        cellSize = cellPrefab.GetComponent<RectTransform>().rect.size.x;
        cells = new BattleCell[fieldSize, fieldSize];
        canvasGroup = GetComponent<CanvasGroup>();
        GenerateField();
    }

    private void OnEnable()
    {
        GenerateShip();
        gameCircle.UpdateCircle((float)chance / 100);
        gameCircle.onCircleStoped.AddListener(() =>
        {
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1f;
        });
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        foreach (BattleCell cell in cells)
        {
            cell.SetDefault();
            cell.secondSprite = seaMineSpite;
        }
    }

    public void GenerateField()
    {
        Vector2 initPosition = new Vector2(-cellSize, -cellSize);

        for (int x = 0; x < fieldSize; x++)
        {
            for (int y = 0; y < fieldSize; y++)
            {
                BattleCell cell = Instantiate(cellPrefab, this.transform).GetComponent<BattleCell>();
                cell.transform.localPosition = new Vector2(initPosition.x + x * cellSize, initPosition.y + y * cellSize);
                cells[x, y] = cell;
                cell.x = x;
                cell.y = y;
                cell.secondSprite = seaMineSpite;
            }
        }
    }

    public void GenerateShip()
    {
        int x = Random.Range(0, fieldSize);
        int y = Random.Range(0, fieldSize);
        cells[x, y].IsShip = true;
        cells[x, y].secondSprite = shipSpite;

        Debug.Log(x + " " + y);
    }

    public void OnCellClick(int x, int y)
    {
        if (cells[x, y].GetComponent<BattleCell>().IsShip)
        {
            Win();
        }
        else
        {
            gameCircle.UpdateCircle((float)chance / 100);

            bool isWin = Random.Range(0f, 100f) <= chance;

            Debug.Log(isWin);

            gameCircle.SpinWheel(isWin);
            chance -= 10;

            canvasGroup.interactable = false;
            canvasGroup.alpha = 0.5f;
        }
    }

    public void Lose()
    {
        uiManager.ShowMiniGameLoseScreen();
        chance = 90;
    }

    public void Win()
    {
        uiManager.ShowMiniGameWinScreen();
        chance = 90;
    }
}
