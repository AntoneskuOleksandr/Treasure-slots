using System.Collections;
using UnityEngine;

public class Column : MonoBehaviour
{
    public GameObject[] cells;
    public float spinSpeed = 1f;
    private bool isSpinning = false;
    private float spinTime;

    private void Start()
    {
        Spin();
    }

    public void Spin()
    {
        if (!isSpinning)
        {
            spinTime = Random.Range(3, 10);
            for (int i = 0; i < cells.Length; i++)
            {
                StartCoroutine(SpinCoroutine(cells[i]));
            }
        }
    }

    private IEnumerator SpinCoroutine(GameObject cell)
    {
        isSpinning = true;

        float tempSpinTime = spinTime;

        while (tempSpinTime > 0)
        {
            cell.transform.localPosition += Vector3.down * spinSpeed * 200 * Time.deltaTime;

            if (cell.transform.localPosition.y <= -600)
            {
                cell.transform.localPosition = new Vector3(cell.transform.localPosition.x, 600, 0);
            }

            tempSpinTime -= Time.deltaTime;

            yield return null;
        }

        isSpinning = false;

        float roundedY = Mathf.Round(cell.transform.localPosition.y / 200) * 200;
        Vector3 roundedPosition = new Vector3(cell.transform.localPosition.x, roundedY, 0);

        // Плавно поставить ячейку на место
        StartCoroutine(MoveToPosition(cell, roundedPosition, 1f));
    }

    private IEnumerator MoveToPosition(GameObject cell, Vector3 targetPosition, float timeToMove)
    {
        Vector3 startPosition = cell.transform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {
            cell.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cell.transform.localPosition = targetPosition;
    }

    public void StopSpin()
    {
        isSpinning = false;
    }
}
