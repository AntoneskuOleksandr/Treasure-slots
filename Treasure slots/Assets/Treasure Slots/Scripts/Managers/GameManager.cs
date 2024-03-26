using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UIManager uiManager;

    private int columnsSpinningCount;
    public int ColumnsSpinningCount
    {
        get
        {
            return columnsSpinningCount;
        }
        set
        {
            columnsSpinningCount = value;

            if (columnsSpinningCount == 0)
                uiManager.OnColumnsStopped();
        }
    }

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }
}
