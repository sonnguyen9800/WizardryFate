using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Damageable))]
[RequireComponent(typeof(SpriteRenderer))]
public class OnDamaged : MonoBehaviour
{

    // Start is called before the first frame update
    private Damageable damageable;


    [Header("Blink Effect")]
    [SerializeField][Range(0.01f, 0.5f)] float  blinkTime;
    public bool isBlinkWhiteOnHit;
    public Material matWhite;


    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;

    [Header("Knockable Effect")]
    public bool isKnockable;
    [Range(0.01f, 0.5f)] public float knockbackAmount;
    private Wizard _playerWizard;

    [Header("Effect on Dead")]
    public Material deadMat;
    [Range(0.1f, 2.5f)] public float deadbackTimeAmount;
    public GameObject _bloodVFX;


    private void Awake() {
        damageable = GetComponent<Damageable>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
        _playerWizard = FindObjectOfType<Wizard>();

        
    }

    void Start()
    {
        if (isBlinkWhiteOnHit){
            damageable.OnDamageTaken += Flash;
        }
        if (isKnockable){
            damageable.OnDamageTaken += KnockBack;
        }
        damageable.OnDead += Die;
    }

    void KnockBack(float _){
        Vector2 direction = -(_playerWizard.transform.position - this.transform.position).normalized;
        this.transform.Translate(direction*knockbackAmount);
    }



    // Update is called once per frame
    void Flash(float x){
            StartCoroutine(Blink());
    }

    public IEnumerator Blink(){
        _spriteRenderer.material = matWhite;
        yield return new WaitForSeconds(blinkTime);
        _spriteRenderer.material = _defaultMaterial;

    }

    void Die(){
        _spriteRenderer.material = deadMat;
        StartCoroutine(BlinkRed());
        //gameObject.SetActive(false);

       



    }

    public IEnumerator BlinkRed(){
        yield return new WaitForSeconds(deadbackTimeAmount);
        if (_bloodVFX != null){
                    GameObject gj = Instantiate(_bloodVFX, this.transform.position, this.transform.rotation);
                    Destroy(gj, 1);
                }

        Destroy(gameObject);
    }



}
