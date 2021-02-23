using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script to attach on ElementVFX game object
public class FollowMainChar : MonoBehaviour
{
    private GameObject _target; //the enemy's target
    private string PLAYER_TAG = "Player";
    [SerializeField] SoulElement soulElement; // Current Soul Element of this game object
    private SoulStealer _playerSouStealer = null;

    private void Awake() {
        _target = GameObject.FindGameObjectWithTag(PLAYER_TAG);
        _playerSouStealer = _target.GetComponent<SoulStealer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag(PLAYER_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerSouStealer.Element != soulElement){
            Destroy(gameObject);
        }

        if (_target == null) return;
        Vector2 targetTransform = _target.transform.position;
        targetTransform.y += 0.9f;
        

        transform.position = Vector3.Lerp(transform.position, targetTransform, Time.time);
        
    }
}
