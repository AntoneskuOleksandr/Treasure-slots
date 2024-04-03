using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsScrene;
    [SerializeField] private GameObject settingButton;

    public void OpenSettings()
    {
        settingsScrene.SetActive(true);
        settingButton.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsScrene.SetActive(false);
        settingButton.SetActive(true);
    }
}
