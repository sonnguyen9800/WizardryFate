using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Text text;
    private bool fadeIn;
    private float _fadeTime;

    private TextMesh textMesh;
    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _fadeTime = 0;
        fadeIn = false;

        StartCoroutine(FadeTextTo(0, 1.5f, textMesh));
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }



    public IEnumerator FadeTextTo(float targetAlpha, float maxDuration, TextMesh textMesh)
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

            // avoid overshooting
            passedTime += Mathf.Min(Time.deltaTime, actualDuration - passedTime);
            yield return null;
        }

        // just to be sure in the end always set it once
        textMesh.color = toColor;
    }
}

