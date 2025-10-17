using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public bool isPlayable = true;
    
    void Awake() {
        Instance = this;
    }
    
    public void EndGame()
    {
        isPlayable = false;
        ResultScreen.Instance.ShowResult(score);
        ResultScreen.Instance.gameObject.SetActive(true);
    }
}