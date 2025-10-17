using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public static ResultScreen Instance;
    public TextMeshProUGUI scoreText;

    void Awake() {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void ShowResult(int score) {
        gameObject.SetActive(true);
        scoreText.text = score.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void QuitMenu()
    {
        SceneManager.LoadScene("Menu");
    } 
}