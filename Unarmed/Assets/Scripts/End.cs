using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Text score;
    public Text highscore;
    public Animator loadlevel;
    // Start is called before the first frame update
    void Start()
    {
        score.text = PlayerPrefs.GetInt("score").ToString();
        highscore.text = PlayerPrefs.GetInt("HighScore").ToString(); ;
    }
    
    public void restart()
    {
        StartCoroutine(levelload());
    }
    IEnumerator levelload()
    {
        loadlevel.SetBool("close", true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void quit()
    {
        Application.Quit();
    }
}
