using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDestroyed : MonoBehaviour
{
    public GameObject destroyedVFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        GameObject game = Instantiate(destroyedVFX, transform.position, transform.rotation);
        Destroy(game, 1);
    }
}
