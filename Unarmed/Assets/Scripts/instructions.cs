using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class instructions : MonoBehaviour
{
    public Animator loadlevel;
    public void LoadLevel()
    {
        StartCoroutine(levelload());
    }

    IEnumerator levelload()
    {
        loadlevel.SetBool("close", true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
