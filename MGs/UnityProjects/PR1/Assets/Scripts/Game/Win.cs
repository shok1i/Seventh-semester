using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (transform.childCount == 1)
        {
            StartCoroutine(LoadScene());
        } 
    }
    
    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
