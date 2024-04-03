using System.Collections;
using TMPro;
using UnityEngine;

public class Column : MonoBehaviour
{
    public Cell[] cells;
    public int[] visibleCells;
    public int minSpinSpeed = 3;
    public int maxSpinSpeed = 10;
    public int minSpinTime = 3;
    public int maxSpinTime = 10;
    public bool isSpining;

    private float cellSize;
    private float spinSpeed;
    private float spinTime;

    private int lastVisibleCellIndex = 0;

    private void Awake()
    {
        cellSize = GlobalVariables.cellSize;
    }

    public void Spin()
    {
        spinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
        spinTime = Random.Range(minSpinTime, maxSpinTime);

        StartCoroutine(SpinColumn());
    }

    private IEnumerator SpinColumn()
    {
        isSpining = true;
        GameManager.Instance.ColumnsSpinningCount++;

        Vector3 targetPosition = transform.position;
        Vector3 initPosition = transform.position;

        targetPosition.y = initPosition.y - cellSize * spinSpeed * spinTime;

        float elapsedTime = 0;
        float t;

        while (elapsedTime < spinTime)
        {
            t = elapsedTime / spinTime;

            transform.position = Vector3.Lerp(initPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, t));

            Cell lastCell = cells[lastVisibleCellIndex];

            if (lastCell.transform.position.y < 360)
            {
                //Debug.Log(lastCell.transform.position.y);
                //Time.timeScale = 0f;

                lastCell.transform.position = new Vector3(lastCell.transform.position.x, lastCell.transform.position.y + cellSize * cells.Length, 0);

                if (lastVisibleCellIndex < cells.Length - 1)
                    lastVisibleCellIndex++;
                else
                    lastVisibleCellIndex = 0;
            }

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = targetPosition;

        cells[lastVisibleCellIndex].transform.position = new Vector3(cells[lastVisibleCellIndex].transform.position.x, 
            cells[lastVisibleCellIndex].transform.position.y + cellSize * cells.Length, 0);

        if (lastVisibleCellIndex < cells.Length - 1)
            lastVisibleCellIndex++;
        else
            lastVisibleCellIndex = 0;


        visibleCells = new int[5];
        for (int i = 0; i < 5; i++)
        {
            visibleCells[4 - i] = (lastVisibleCellIndex + i) % cells.Length;
        }

        isSpining = false;
        GameManager.Instance.ColumnsSpinningCount--;

    }
}
