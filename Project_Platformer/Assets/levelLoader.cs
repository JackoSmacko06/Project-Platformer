using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{

    public static levelLoader Instance;

    public GameObject transitionBlack;

    public GameObject transitionWhite;

    public float transitionTime = 1;


    private void Start()
    {
        Instance = this;

        transitionWhite.SetActive(false);
    }
    void Update()
    {

    }

    public void nextLevelBlack()
    {
        StartCoroutine(LoadLevelBlack(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void nextlevelWhite()
    {
        StartCoroutine(LoadLevelWhite(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevelBlack(int levelindex)
    {
        transitionBlack.SetActive(true);
        transitionBlack.GetComponent<Animator>().SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelindex);
    }

    IEnumerator LoadLevelWhite(int levelindex)
    {
        transitionWhite.SetActive(true);
        transitionWhite.GetComponent<Animator>().SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelindex);
    }
}
