using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MiniGameCircle : MonoBehaviour
{
    [SerializeField] private MiniGameField gameField;
    [SerializeField] private GameObject wheel;
    [SerializeField] private Image fillingCircle;
    [SerializeField] private float minSpinTime;
    [SerializeField] private float maxSpinTime;

    public UnityEvent onCircleStoped;

    public void UpdateCircle(float newFillAmount)
    {
        fillingCircle.fillAmount = newFillAmount;
    }

    public void SpinWheel(bool result)
    {
        StartCoroutine(SpinWheelToResult(result));
    }

    private IEnumerator SpinWheelToResult(bool result)
    {
        float startAngle = wheel.transform.eulerAngles.z;

        float minSpinRotation = 0f;
        float maxSpinRotation = fillingCircle.fillAmount * 360;

        float spinTime = Random.Range(minSpinTime, maxSpinTime);
        int fullRotations = Random.Range(1, 3);

        float targetRotation;

        if (result)
            targetRotation = Random.Range(minSpinRotation, maxSpinRotation);
        else
            targetRotation = Random.Range(maxSpinRotation, 360);

        targetRotation += fullRotations * 360;

        float spinSpeed = Mathf.Abs(targetRotation - startAngle) / spinTime;

        float elapsedTime = 0;
        float currentRotation;
        while (elapsedTime < spinTime)
        {
            currentRotation = spinSpeed * Time.deltaTime;
            wheel.transform.Rotate(0, 0, currentRotation);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        float finalRotation = targetRotation - wheel.transform.eulerAngles.z;
        wheel.transform.Rotate(0, 0, finalRotation);

        onCircleStoped.Invoke();

        if (!result)
            gameField.Lose();
    }
}