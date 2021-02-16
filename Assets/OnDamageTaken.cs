using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDamageTaken : MonoBehaviour
{
    private Damageable damageable;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        damageable = GetComponent<Damageable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        damageable.OnDamageTaken += swiftBlink;
    }
    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;

    public float spriteBlinkingTotalDuration = 2.0f; // Total duration for the blinking
    public bool startBlinking = false;
    void Update()
    {

        if (startBlinking)
        {
            SpriteBlinkingEffect();
        }
    }

    void swiftBlink(float _){
        startBlinking = true;
    }

    private void SpriteBlinkingEffect()
    {
        damageable.isInvicible = true; // Turn on Invicible
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            damageable.isInvicible = false;
            spriteBlinkingTotalTimer = 0.0f;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;   // according to 
                                                                             //your sprite
            startBlinking = false;
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
        }
    }


    
}
