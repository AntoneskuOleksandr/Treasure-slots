using UnityEngine;

public class CombinationManager : MonoBehaviour
{
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private UIManager UIManager;

    public int bonusCount = 0;
    public int combinationLength = 1;

    public void CheckCombinations()
    {
        bool[,] countedInCombination = new bool[gridGenerator.width, gridGenerator.columns[0].visibleCells.Length];

        for (int y = 0; y < gridGenerator.columns[0].visibleCells.Length; y++)
        {
            Cell.Symbol firstSymbol = gridGenerator.columns[0].cells[gridGenerator.columns[0].visibleCells[y]].symbol;

            combinationLength = 1;
            bonusCount = 0;

            for (int x = 1; x < gridGenerator.width; x++)
            {
                Cell.Symbol currentSymbol = gridGenerator.columns[x].cells[gridGenerator.columns[x].visibleCells[y]].symbol;

                if (currentSymbol == firstSymbol && !countedInCombination[x, y])
                {
                    combinationLength++;
                    countedInCombination[x, y] = true;
                }
                else
                {
                    if (combinationLength > 1)
                    {
                        CheckForBonusSymbols(combinationLength, y, firstSymbol, countedInCombination);
                        Debug.Log($"A combination of {combinationLength} characters {firstSymbol} on the line {y} with bonus: {bonusCount}");
                        UIManager.OnCombinationFound(combinationLength + bonusCount);
                    }

                    combinationLength = 1;
                    break;
                }
            }

            if (combinationLength > 1)
            {
                CheckForBonusSymbols(combinationLength, y, firstSymbol, countedInCombination);
                Debug.Log($"A combination of {combinationLength} characters {firstSymbol} on the line {y} with bonus: {bonusCount}");
                UIManager.OnCombinationFound(combinationLength + bonusCount);
            }
        }
    }

    private void CheckForBonusSymbols(int symbolsCount, int rowIndex, Cell.Symbol symbol, bool[,] countedInCombination)
    {
        for (int y = 0; y < gridGenerator.columns[0].visibleCells.Length; y++)
        {
            if (y == rowIndex)
                continue;

            for (int x = 0; x < symbolsCount; x++)
            {
                if (gridGenerator.columns[x].cells[gridGenerator.columns[x].visibleCells[y]].symbol == symbol && !countedInCombination[x, y])
                {
                    bonusCount++;
                    countedInCombination[x, y] = true;
                }
            }
        }
    }
}
