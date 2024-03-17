using System.Collections;
using UnityEngine;

public class Column : MonoBehaviour
{
    public GameObject[] cells;
    public int minSpinSpeed = 3;
    public int maxSpinSpeed = 10;
    public int minSpinTime = 3;
    public int maxSpinTime = 10;

    private float cellSize;
    private float spinSpeed;
    private float spinTime;

    private void Start()
    {
        spinSpeed = Random.Range(minSpinSpeed, maxSpinSpeed);
        spinTime = Random.Range(minSpinTime, maxSpinTime);

        Spin();
    }

    public void Spin()
    {
        cellSize = GlobalVariables.cellSize;
        StartCoroutine(SpinColumn());
    }

    private IEnumerator SpinColumn()
    {
        int lastCellIndex = 0;
        Vector3 targetPosition = transform.position;
        Vector3 initPosition = transform.position;

        targetPosition.y = initPosition.y - cellSize * spinSpeed * spinTime;

        float elapsedTime = 0;
        float t;

        while (elapsedTime < spinTime)
        {
            t = elapsedTime / spinTime;

            transform.position = Vector3.Lerp(initPosition, targetPosition, Mathf.SmoothStep(0.0f, 1.0f, t));

            GameObject lastCell = cells[lastCellIndex];

            if (lastCell.transform.position.y <= 360)
            {
                lastCell.transform.position = new Vector3(lastCell.transform.position.x, lastCell.transform.position.y + cellSize * cells.Length, 0);

                if (lastCellIndex < cells.Length - 1)
                    lastCellIndex++;
                else
                    lastCellIndex = 0;
            }

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = targetPosition;
    }
}
