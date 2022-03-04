using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Animator loadlevel;
    public void LoadLevel()
    {
        if(PlayerPrefs.GetInt("HighScore") == 0)
        {
            StartCoroutine(loadInstructions());
        }
        else
        {
            StartCoroutine(levelload());
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator levelload()
    {
        loadlevel.SetBool("close", true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    IEnumerator loadInstructions()
    {
        loadlevel.SetBool("close", true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
