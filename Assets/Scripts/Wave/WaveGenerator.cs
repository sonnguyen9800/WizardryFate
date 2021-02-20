﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] monstersPrefab;

    int difficultLevel = 0; // Increased by time, each level of difficulity increased the spawn rate
    [SerializeField] private float  _range = 10f; // Range to spawn monster; only spawn when player is near

    private float _currentTimer = 0; // Timer reache a threshold -> increase difficultity level

    [SerializeField] private float InitialSpawnInterval = 10f;
    [SerializeField] private float _thresholdTime = 20f;


    private float _spawnTimer = 0;
    private float _spawnTimerRate = 5f; // Timer interval between each spawn

    private GameObject _player ;
    System.Random rand = new System.Random();  

    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player");
        _spawnTimerRate = InitialSpawnInterval;
    }

    void Start()
    {
        
    }

    private void reduceTimeIntervalBetweenSpawn(){
        _spawnTimerRate = _spawnTimerRate*Mathf.Pow(0.95f, difficultLevel);
    }
    // Update is called once per frame
    void Update()
    {
        _currentTimer += Time.deltaTime;
        if (_currentTimer >= _thresholdTime){
            difficultLevel ++;
            _currentTimer=0;
            reduceTimeIntervalBetweenSpawn();
        }

        if (_spawnTimer > 0) { _spawnTimer -= Time.deltaTime; return;}
        if (Vector2.Distance(transform.position, _player.transform.position) <= _range){
            Instantiate(monstersPrefab[rand.Next(monstersPrefab.Length)], transform.position, Quaternion.identity);
            _spawnTimer = _spawnTimerRate;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}