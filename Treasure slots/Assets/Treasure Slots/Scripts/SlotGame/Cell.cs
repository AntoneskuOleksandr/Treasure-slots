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
        AmethystScarab = 0,
        EmeraldAnkh = 1,
        LapisLazuliObelisk = 2,
        RubyScarab = 3,
        SunFire = 4,
        TopazEyeOfHorus = 5
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
