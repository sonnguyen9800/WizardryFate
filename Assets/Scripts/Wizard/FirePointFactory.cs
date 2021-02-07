using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName= "FirePointFactory")]

public class FirePointFactory : ScriptableObject
{
    [System.Serializable]
    public class FirePoint{
        public WizardState _state;
        public Transform _firepoint;
    }
    [SerializeField] private FirePoint[] firePoints;
    private Dictionary<WizardState, FirePoint> FirepointAni = 
        new Dictionary<WizardState, FirePoint>();

    private void OnEnable()
    {
        foreach (var elementInfo in firePoints)
        {
            FirepointAni[elementInfo._state] = elementInfo; // Initialize the Dictionary
        }
    }

    public FirePoint GetFirePoint(WizardState state){
        return FirepointAni[state];
    }

    public Transform GetFirePointTransform(WizardState state){
        return FirepointAni[state]._firepoint;
    }
}
