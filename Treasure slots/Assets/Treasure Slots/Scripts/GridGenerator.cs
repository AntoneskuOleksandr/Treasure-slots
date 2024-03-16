using TMPro;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject columnPrefab; // Добавьте это

    [Header("Grid Size")]
    public int width = 5;
    public int height = 5;

    public GameObject[,] cells;
    public GameObject[] columns; // Добавьте это
    public float cellSize;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        RectTransform rectTransform = cellPrefab.GetComponent<RectTransform>();

        cellSize = rectTransform.rect.size.x;

        Vector2 initPosition = new Vector2(-cellSize * ((width - 1) / 2), -cellSize * ((height - 1) / 2));

        cells = new GameObject[width, height];
        columns = new GameObject[width]; // Инициализируйте это

        int index = 0;

        for (int x = 0; x < width; x++)
        {
            // Создайте новый столбец для каждого x
            GameObject column = Instantiate(columnPrefab, this.transform);
            columns[x] = column;
            Column columnScript = column.GetComponent<Column>();
            columnScript.cells = new GameObject[height];

            for (int y = 0; y < height; y++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector2(initPosition.x + x * cellSize, initPosition.y + y * cellSize), Quaternion.identity);
                cell.transform.SetParent(column.transform, false);

                cells[x, y] = cell;
                columnScript.cells[y] = cell; // Добавьте ячейку в столбец

                cell.GetComponentInChildren<TMP_Text>().text = index.ToString();
                index++;
            }
        }
    }
}
