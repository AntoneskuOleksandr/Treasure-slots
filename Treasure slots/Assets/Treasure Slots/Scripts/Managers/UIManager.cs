using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private CombinationManager combinationManager;
    [SerializeField] private MoneyManager moneyManager;

    [Header("Progress Bar")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text progressBarText;
    [SerializeField] private float maxProgressBarValue;

    [Header("Buttons")]
    [SerializeField] private Button rollSlotMachineButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text moneyValue;

    [Header("Bet")]
    [SerializeField] private TMP_Text betText;
    [SerializeField] private Button descreaseBet;
    [SerializeField] private Button increaseBet;
    [SerializeField] private float betStep;
    [SerializeField] private float defaultBet;
    [SerializeField] private float minBet;

    private Column[] columns;
    private float betAmount;

    private void Awake()
    {
        rollSlotMachineButton.onClick.AddListener(OnStartRolling);
        moneyManager.onMoneyChanged.AddListener(UpdateUI);

        increaseBet.onClick.AddListener(IncreaseBet);
        descreaseBet.onClick.AddListener(DecreaseBet);

        progressBarText.text = 0 + "%";
        betAmount = defaultBet;
        betText.text = defaultBet.ToString();
        UpdateUI();
    }

    public void OnStartRolling()
    {
        if (moneyManager.Money < betAmount)
        {
            Debug.LogError("We have less money than bet");
            return;
        }

        moneyManager.Money -= betAmount;

        gridGenerator.GenerateNewGrid();

        columns = gridGenerator.columns;

        foreach (Column column in columns)
            column.Spin();

        UpdateButtons();
        rollSlotMachineButton.interactable = false;
    }

    public void OnColumnsStopped()
    {
        combinationManager.CheckCombinations();
        UpdateButtons();
    }

    public void OnCombinationFound(float combinationValue)
    {
        ChangeProgressBarValue(combinationValue);
        moneyManager.Money += combinationValue;
    }

    private void ChangeProgressBarValue(float value)
    {
        float newValue = value / maxProgressBarValue * 100;

        if (newValue < 0) newValue = 0;
        else if (newValue > 100) newValue = 100;

        progressBar.value += newValue;
        progressBarText.text = (progressBar.value).ToString("F1") + "%";
    }

    private void UpdateUI()
    {
        moneyValue.text = moneyManager.Money.ToString() + "$";
        UpdateButtons();
    }

    private void IncreaseBet()
    {
        if (betAmount + betStep <= moneyManager.Money)
        {
            betAmount += betStep;
            betText.text = betAmount.ToString();
        }
        UpdateButtons();
    }

    private void DecreaseBet()
    {
        if (betAmount - betStep >= minBet)
        {
            betAmount -= betStep;
            betText.text = betAmount.ToString();
        }
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        increaseBet.interactable = betAmount + betStep <= moneyManager.Money;
        descreaseBet.interactable = betAmount - betStep >= minBet;
        rollSlotMachineButton.interactable = moneyManager.Money >= betAmount;
    }
}
