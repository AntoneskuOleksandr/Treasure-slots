using UnityEngine;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    public UnityEvent onMoneyChanged;
        
    [SerializeField] private float money;
    public float Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            onMoneyChanged.Invoke();
        }
    }
}
