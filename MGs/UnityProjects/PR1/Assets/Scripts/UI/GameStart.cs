using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerCount
{
    public static int Number;
}

public class GameStart : MonoBehaviour
{
    public void OnClick_TwoPlayers()
    {
        PlayerCount.Number = 2;
        SceneManager.LoadScene("Game");
    }

    public void OnClick_ThreePlayers()
    {
        PlayerCount.Number = 3;
        SceneManager.LoadScene("Game");
    }

    public void OnClick_FourPlayers()
    {
        PlayerCount.Number = 4;
        SceneManager.LoadScene("Game");
    }
}
