using TMPro;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject columnPrefab;

    [Header("Grid Size")]
    public int width = 5;
    public int height = 5;

    public GameObject[,] cells;
    public GameObject[] columns;

    private float cellSize;

    private void Awake()
    {
        GlobalVariables.cellSize = cellPrefab.GetComponent<RectTransform>().rect.size.x;

        cellSize = GlobalVariables.cellSize;

        Vector2 initPosition = new Vector2(-cellSize * ((width - 1) / 2), -cellSize * ((height - 1) / 2));

        cells = new GameObject[width, height];
        columns = new GameObject[width];

        int index = 0;

        for (int x = 0; x < width; x++)
        {
            GameObject column = Instantiate(columnPrefab, this.transform);
            columns[x] = column;
            Column columnScript = column.GetComponent<Column>();
            columnScript.cells = new GameObject[height];

            for (int y = 0; y < height; y++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector2(initPosition.x + x * cellSize, initPosition.y + y * cellSize), Quaternion.identity);
                cell.transform.SetParent(column.transform, false);

                cells[x, y] = cell;
                columnScript.cells[y] = cell;

                cell.GetComponentInChildren<TMP_Text>().text = index.ToString();
                index++;
            }
        }
    }
}