using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private Button rollSlotMachineButton;

    private Column[] columns;

    private void Awake()
    {
        rollSlotMachineButton.onClick.AddListener(OnStartRolling);
    }

    public void OnStartRolling()
    {
        columns = gridGenerator.columns;

        foreach(Column column in columns)
        {
            column.Spin();
        }

        rollSlotMachineButton.interactable = false;
    }

    public void OnColumnsStoped()
    {
        rollSlotMachineButton.interactable = true;
    }
}
