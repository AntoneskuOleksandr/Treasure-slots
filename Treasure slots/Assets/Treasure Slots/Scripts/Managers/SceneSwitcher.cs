using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string mainMenuScene;
    [SerializeField] private string gameScene;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void OpenGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}
