using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Cell : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    public Symbol symbol;

    private Image image;

    public enum Symbol
    {
        Apple = 0,
        Pear = 1,
        Cherry = 2,
        Banana = 3,
        Lemon = 4,
        Grapes = 5
    }

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetRandomSymbol()
    {
        int randomIndex = Random.Range(0, Enum.GetValues(typeof(Symbol)).Length);
        symbol = (Symbol)randomIndex;
        image.sprite = sprites[randomIndex];
    }
}
