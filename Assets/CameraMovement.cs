using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _mainCamera;

    [SerializeField] float pixelMovementSpeed = 1.2f;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _mainCamera.transform.Translate(new Vector3(pixelMovementSpeed, 0, 0) * Time.deltaTime);
    }
}
