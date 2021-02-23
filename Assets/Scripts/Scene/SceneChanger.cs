using UnityEngine;
using UnityEngine.SceneManagement;

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

    public SceneName sceneName;
    // Start is called before the first frame update

    void SwitchScene(SceneName sceneName)
    {
        SceneManager.LoadScene((int)sceneName);
    }

  
}
