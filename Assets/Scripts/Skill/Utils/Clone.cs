using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    [SerializeField] private GameObject _vfxEffect;
    private GameObject go;
    private void Awake()
    {
        
    }

    private void Start()
    {
        go = Instantiate(_vfxEffect, transform.position, Quaternion.identity);

    }

    private void OnDestroy()
    {
        Destroy(go);
    }


}
