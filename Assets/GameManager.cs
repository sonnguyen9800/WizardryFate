﻿using System.Collections;
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
    [SerializeField] bool disableWave = false;

    private GameObject[] waveGeneratorsGameObject;

    [Header("Audio")]
    public AudioClip background;
    private AudioSource _audioSource;

    [Header("Time Indicator")]
    [SerializeField] public TMPro.TextMeshProUGUI timeIndicator;

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
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        InvokeRepeating("UpdateCounter", 1f, 1f);
        if (_audioSource == null || background == null) return;
        _audioSource.loop = true;
        _audioSource.clip = background;
        _audioSource.Play();
    }

    private void UpdateCounter()
    {
        _currentTimer++;
        timeIndicator.text = "Time Left: " + (victoryTimer - _currentTimer).ToString();
        //_currentTimer += Time.deltaTime;
        if (_currentTimer >= victoryTimer && _player != null)
        {
            SceneManager.LoadSceneAsync("VictoryScene");
        }
        else if (_player == null)
        {
            //Debug.Log("Defeated");
            // Chance Scene
            SceneManager.LoadSceneAsync("DefeatScene");

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
