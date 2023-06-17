using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{ 
    [Header("Variables")]
    public Animator animator;

    private int levelToLoad;
    [Range(0.1f,1.0f)]
    public float fadeRate;
    [Space]

    [Header("Checks")]
    public bool gymTrigger_active;
    public bool rangeTrigger_active;
    public bool derbyTrigger_active;

    // Update is called once per frame
    void Update ()
    {
        //Resetter
        if (Input.GetMouseButton(2))
        {
            FadeToLevel(0);
        }
        //Trigger Checks
        if (gymTrigger_active == true && Input.GetButton("Interact"))
        {
            FadeToLevel(1);
        }

        if (rangeTrigger_active == true && Input.GetButton("Interact"))
        {
            FadeToLevel(2);
        }

        if (derbyTrigger_active == true && Input.GetButton("Interact"))
        {
            FadeToLevel(3);
        }
    }

    public void FadeToLevel (int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete ()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        //Fade-in timer between scenes; could be random between a specific range?
        yield return new 
            WaitForSeconds(Random.Range(0.1f, fadeRate));
        animator.SetTrigger("FadeIn");
        SceneManager.LoadScene(levelToLoad);
    }
}
