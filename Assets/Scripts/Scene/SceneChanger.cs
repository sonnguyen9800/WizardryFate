using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
//public class SceneName
//{
//    private SceneName(string value) { Value = value; }

//    public string Value { get; set; }

//    public static SceneName MainScene   { get { return new SceneName("MainScene"); } }
//    public static SceneName PlayScene   { get { return new SceneName("PlayScene"); } }
//    public static SceneName TutorialScene    { get { return new SceneName("TutorialScene"); } }
//    public static SceneName DefeatScene { get { return new SceneName("DefeatScene"); } }
//    public static SceneName VictoryScene   { get { return new SceneName("VictoryScene"); } }
//}

public enum SceneName
{
    MainScene,
    PlayScene,
    TutorialScene,
    DefeatScene,
    VictoryScene
}


public class SceneChanger : MonoBehaviour
{
    [SerializeField] public GameObject ImageFilter; // The Image filter needed to create transition effect
    public SceneName sceneName;
    // Start is called before the first frame update
    Animator animator;

    private void Awake()
    {
        animator = ImageFilter.GetComponent<Animator>();
        if (animator == null)
        {
            print("No animator found");
        }
    }
    public void LoadScene()
    {
        StartCoroutine(WaitTransition());
    }



    IEnumerator WaitTransition()
    {
        animator.SetTrigger("Start");
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene((int)sceneName);

    }

    IEnumerator WaitTransitionToQuit()
    {
        animator.SetTrigger("Start");
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }


    public void QuitGame()
    {
        StartCoroutine(WaitTransitionToQuit());
    }
  
}
