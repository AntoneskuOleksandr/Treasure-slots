using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Scenes")]
    [SerializeField] private string MainMenu = "MainMenu";
    [SerializeField] private Button mainMenuButton;

    [Header("SlotMachine")]
    [SerializeField] private GameObject slotGame;
    [SerializeField] private GameObject winScreen;

    [Header("Components")]
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private CombinationManager combinationManager;
    [SerializeField] private MoneyManager moneyManager;

    [Header("Progress Bar")]
    [SerializeField] private Slider progressBar;
    [SerializeField] private TMP_Text progressBarText;
    [SerializeField] private Gradient gradient;
    [SerializeField] private float maxProgressBarValue;

    [Header("Buttons")]
    [SerializeField] private Button rollSlotMachineButton;

    [Header("Texts")]
    [SerializeField] private TMP_Text moneyText;

    [Header("Bet")]
    [SerializeField] private TMP_Text betText;
    [SerializeField] private Button descreaseBetButton;
    [SerializeField] private Button increaseBetButton;
    [SerializeField] private Button minBetButton;
    [SerializeField] private Button maxBetButton;
    [SerializeField] private float betStep;
    [SerializeField] private float defaultBet;
    [SerializeField] private float minBet;

    [Header("MiniGame")]
    [SerializeField] private GameObject miniGame;
    [SerializeField] private GameObject miniGameLoseScreen;
    [SerializeField] private GameObject miniGameWinScreen;

    private Column[] columns;
    private float betAmount;
    private float bettedBet;
    private bool isSpinning;

    private void Awake()
    {
        rollSlotMachineButton.onClick.AddListener(OnStartRolling);
        moneyManager.onMoneyChanged.AddListener(UpdateUI);

        increaseBetButton.onClick.AddListener(IncreaseBet);
        descreaseBetButton.onClick.AddListener(DecreaseBet);

        minBetButton.onClick.AddListener(SetMinBet);
        maxBetButton.onClick.AddListener(SetMaxBet);

        mainMenuButton.onClick.AddListener(OpenMainMenu);

        SetDefaultValues();

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
        bettedBet = betAmount;

        isSpinning = true;

        gridGenerator.GenerateNewGrid();

        columns = gridGenerator.columns;

        foreach (Column column in columns)
            column.Spin();

        UpdateButtons();
        rollSlotMachineButton.interactable = false;
    }

    public void OnColumnsStopped()
    {
        isSpinning = false;
        combinationManager.CheckCombinations();
        UpdateButtons();
    }

    public void OnCombinationFound(float combinationValue)
    {
        moneyManager.Money += combinationValue;
        ChangeProgressBarValue(combinationValue * bettedBet / defaultBet);
    }

    private void ChangeProgressBarValue(float value, bool resetValue = false)
    {
        if (resetValue)
            progressBar.value = 0;
        else
        {
            float newValue = value / maxProgressBarValue * 100;

            progressBar.value += newValue;

            if (progressBar.value >= 100)
            {
                progressBar.value = 100;
                ProgressBarFull();
            }
        }

        Color textColor = gradient.Evaluate(progressBar.value / 100);
        progressBarText.color = textColor;

        progressBarText.text = progressBar.value.ToString("F1") + "%";
    }

    private void ProgressBarFull()
    {
        ShowGameWinScreen(true);
    }


    private void UpdateUI()
    {
        moneyText.text = moneyManager.Money.ToString();
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

    private void SetMinBet()
    {
        betAmount = minBet;
        betText.text = betAmount.ToString();

        UpdateButtons();
    }

    private void SetMaxBet()
    {
        if (moneyManager.Money >= minBet)
        {
            betAmount = moneyManager.Money;
            betText.text = betAmount.ToString();
        }

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        increaseBetButton.interactable = betAmount + betStep <= moneyManager.Money;
        descreaseBetButton.interactable = betAmount - betStep >= minBet;
        rollSlotMachineButton.interactable = moneyManager.Money >= betAmount && !isSpinning;
    }

    public void ShowMiniGame(bool isShow)
    {
        if (!isShow)
        {
            SetDefaultValues();
        }

        miniGame.SetActive(isShow);
        slotGame.SetActive(!isShow);

        miniGameLoseScreen.SetActive(false);
        miniGameWinScreen.SetActive(false);
    }

    public void ShowMiniGameLoseScreen()
    {
        miniGameLoseScreen.SetActive(true);
    }

    public void ShowMiniGameWinScreen()
    {
        miniGameWinScreen.SetActive(true);
        moneyManager.Money += 1000;
    }

    private void ShowGameWinScreen(bool isShow)
    {
        winScreen.SetActive(isShow);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    private void SetDefaultValues()
    {
        ChangeProgressBarValue(0, true);
        ShowGameWinScreen(false);
        betAmount = defaultBet;
        betText.text = defaultBet.ToString();

        combinationManager.bonusCount = 0;
        combinationManager.combinationLength = 1;
    }
}
