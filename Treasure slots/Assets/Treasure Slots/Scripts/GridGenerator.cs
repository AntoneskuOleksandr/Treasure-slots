using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject cellPrefab;

    [Header("Grid Size")]
    [SerializeField] private int width = 5;
    [SerializeField] private int height = 5;

    private void Start()
    {
        RectTransform rectTransform = cellPrefab.GetComponent<RectTransform>();
        float cellWidth = rectTransform.rect.width;
        float cellHeight = rectTransform.rect.height;

        Vector2 initPosition = new Vector2(-cellWidth * ((width - 1) / 2), -cellHeight * ((height - 1) / 2));

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector2(initPosition.x + x * cellWidth, initPosition.y + y * cellHeight), Quaternion.identity);
                cell.transform.SetParent(this.transform, false);
            }
        }
    }
}
