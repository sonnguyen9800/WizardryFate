using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    //private List<WaveGenerator> waveGenerators = new List<WaveGenerator>();
    private float _currentTimer = 0f;

    [SerializeField] private float victoryTimer = 600f;

    private GameObject _player;
    private Damageable damageable;
    private GameObject[] waveGeneratorsGameObject;


    [Header("Time Indicator")]
    [SerializeField] public TMPro.TextMeshProUGUI timeIndicator;

    [Header("Transition Animation")]
    [SerializeField]public Animator animator;

    public bool victory = false;
    private void OnEnable() {
        waveGeneratorsGameObject = GameObject.FindGameObjectsWithTag("WaveMonster");
        //print("Found " + waveGeneratorsGameObject.Length + " objects");
    }
    private void Awake() {
        // foreach (var gameobject in waveGeneratorsGameObject){
        //     //print("Find");
        //     WaveGenerator wave = gameObject.GetComponent<WaveGenerator>();
        //     waveGenerators.Add(wave);
        // }
        _player = GameObject.FindGameObjectWithTag("Player");
        damageable = _player.GetComponent<Damageable>();
    }

    void Start()
    {
        InvokeRepeating("UpdateCounter", 1f, 1f);

    }

    private void UpdateCounter()
    {
        _currentTimer++;
        timeIndicator.text = (victoryTimer - _currentTimer) >= 0 ? "Time Left: " + (victoryTimer - _currentTimer).ToString() : "Timeout"  ;

        if (victory == true)
        {
            StartCoroutine(WaitForVictory());
            return;
        }
        if (_currentTimer >= victoryTimer && _player != null)
        {
            //ClearItems("Item");
            //StartCoroutine(WaitForVictory());
            StartCoroutine(WaitForDefeat());
        }
        else if (_player == null)
        {
            //ClearItems("Item");
            StartCoroutine(WaitForDefeat());

        }
    }


    IEnumerator WaitForDefeat()
    {
        animator.SetTrigger("Dying");
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("DefeatScene");

    }

    IEnumerator WaitForVictory()
    {
        animator.SetTrigger("Victory");
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync("VictoryScene");

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }

    private void ClearItems(string tag)
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag(tag);
        print("Found " + items.Length);
        foreach(var item in items)
        {
            Destroy(item);
        }
    }
    private void swiftWaves(bool status){
       //print("Lenght" + waveGeneratorsGameObject.Length);
        for(int i = 0;i < waveGeneratorsGameObject.Length; i++){
            //print(waveGeneratorsGameObject[i].name);
            waveGeneratorsGameObject[i].SetActive(status);
        }
    }
    
 
    // Update is called once per frame
    void Update()
    {
        //swiftWaves(!disableWave);
        
        
        
    }


}
