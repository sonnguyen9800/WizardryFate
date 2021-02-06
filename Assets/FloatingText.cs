using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to create floating text for damage popup / status ailment info

public class FloatingText : MonoBehaviour
{   
    [SerializeField] public LayerMask layer;
    
    public string layerToPushTo;

	void Start () 
    {
        //Debug.Log(GetComponent<Renderer>().sortingLayerName);
	}
    private void Awake() {
        GetComponent<Renderer>().sortingLayerName = layerToPushTo;

        
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
       //int id = LayerMask.NameToLayer("Effect");

        //_meshRenderer.sortingLayerID = id;

    }
}
