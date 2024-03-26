using System;
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

    public Cell[,] cells;
    public Column[] columns;

    private float cellSize;

    private void Awake()
    {
        GlobalVariables.cellSize = cellPrefab.GetComponent<RectTransform>().rect.size.x;

        cellSize = GlobalVariables.cellSize;

        cells = new Cell[width, height];
        columns = new Column[width];

        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector2 initPosition = new Vector2(-cellSize * ((width - 1) / 2), -cellSize * 2);

        for (int x = 0; x < width; x++)
        {
            Column column = Instantiate(columnPrefab, this.transform).GetComponent<Column>();
            columns[x] = column;
            column.cells = new Cell[height];

            for (int y = 0; y < height; y++)
            {
                Cell cell = Instantiate(cellPrefab, new Vector2(initPosition.x + x * cellSize, initPosition.y + y * cellSize),
                    Quaternion.identity).GetComponent<Cell>();
                cell.transform.SetParent(column.transform, false);

                cell.SetRandomSymbol();

                cell.GetComponentInChildren<TMP_Text>().text = $"({x};{y})";

                cells[x, y] = cell;
                column.cells[y] = cell;
            }
        }
    }

    public void GenerateNewGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                cells[x, y].SetRandomSymbol();
            }
        }
    }
}