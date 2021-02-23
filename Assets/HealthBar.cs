using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private GameObject _wizard;
    private Damageable _wizardDamageable;
    private Slider slider;
    private void Awake()
    {
        _wizard = GameObject.FindGameObjectWithTag("Player");
        _wizardDamageable = _wizard.GetComponent<Damageable>();
        slider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_wizardDamageable == null) return;
        _wizardDamageable.OnHealthChanged += RenderHealth;
    }

    private void RenderHealth()
    {
        if (slider == null || _wizardDamageable == null) return;
        slider.value = _wizardDamageable.Percentage;
    }

    // Update is called once per frame
    void Update()
    {
        if (_wizard == null) Destroy(gameObject);
    }
}
