using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFirePoint : MonoBehaviour
{
    private Transform _firepoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setFirepoint(Transform game){
        this._firepoint = game;
    }

    // Update is called once per frame
    void Update()
    {
        if (_firepoint == null) return;
        // Update the transform position
        transform.position = _firepoint.transform.position;
        transform.rotation= _firepoint.transform.rotation;
    }
}
