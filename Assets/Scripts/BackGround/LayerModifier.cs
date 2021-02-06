using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerModifier : MonoBehaviour
{
    public string layerToPushTo;

    private void Awake() {
            GetComponent<Renderer>().sortingLayerName = layerToPushTo;
    }
    // Start is called before the first frame update
}
