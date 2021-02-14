using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class FlyingZone
{
    public float MaxY;
    public float MinY;
    public float MaxX;
    public float MinX;

}


public class MadLoveAI : EnemyAI
{
    // Start is called before the first frame update

    [Header("Extra Properties")]

    [SerializeField]
    private FlyingZone zone; // Working Zone of the system



    public override void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath
            (transform.position, player.transform.position, onPathComplete);
        }
    }
}
