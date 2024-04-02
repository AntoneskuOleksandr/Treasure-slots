using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingsScrene;

    public void OpenSettings()
    {
        SettingsScrene.SetActive(true);
    }

    public void CloseSettings()
    {
        SettingsScrene.SetActive(false);
    }
}
