using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SoundEffectManager.Play("Ding");
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        SoundEffectManager.Play("Ding");
        Application.Quit();
    }
}
