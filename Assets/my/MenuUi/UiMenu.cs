using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UiMenu : MonoBehaviour
{   
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject settingsPanel;
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Settings(bool value)
    {
        mainPanel.SetActive(!value);
        settingsPanel.SetActive(value);
    }
    public void SetQuality(int value)
    {
        QualitySettings.SetQualityLevel(value, true);
    }
}
