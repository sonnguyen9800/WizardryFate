using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to create floating text for damage popup / status ailment info

public class FloatingText : MonoBehaviour
{   
    public string layerToPushTo;
    private float moveUpSpeed = 0.8f;
    private TMPro.TextMeshPro textMesh;
    private float _fadeTime;

    [SerializeField]
    public string content;

	void Start () 
    {
        //Debug.Log(GetComponent<Renderer>().sortingLayerName);
        _fadeTime = 1.5f;
        StartCoroutine(FadeTextTo(0, _fadeTime, textMesh));
    }
    private void Awake() {
        GetComponent<Renderer>().sortingLayerName = "Projectile";
        textMesh = GetComponent<TMPro.TextMeshPro>();
        textMesh.text = content;
    }
    public TMPro.TextMeshPro returnTMPro()
    {
        return textMesh;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up*moveUpSpeed*Time.deltaTime);
    }

    public IEnumerator FadeTextTo(float targetAlpha, float maxDuration, TMPro.TextMeshPro textMesh)
    {
        // more efficient to get both colors beforehand
        var fromColor = textMesh.color;
        var toColor = new Color(fromColor.r, fromColor.g, fromColor.b, targetAlpha);

        // this is optional ofcourse but I like to do this in order to
        // always have the same fading speed even if it is already slightly faded into one direction
        var actualDuration = maxDuration * Mathf.Abs(fromColor.a - toColor.a);

        var passedTime = 0f;
        while (passedTime < actualDuration)
        {
            var lerpFactor = passedTime / actualDuration;
            // now the huge advantage is that you can add ease-in and -out if you like e.g.
            //lerpFactor = Mathf.SmoothStep(0, 1, lerpFactor);

            textMesh.color = Color.Lerp(fromColor, toColor, lerpFactor);
            transform.Translate(Vector2.up*Time.deltaTime*moveUpSpeed);

            // avoid overshooting
            passedTime += Mathf.Min(Time.deltaTime, actualDuration - passedTime);
            yield return null;
        }

        // just to be sure in the end always set it once
        textMesh.color = toColor;
        Destroy(gameObject);
    }
}
