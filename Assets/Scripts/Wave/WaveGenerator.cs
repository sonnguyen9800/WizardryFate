using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterPool
{
    [SerializeField] public GameObject monsterPrefab;
    [SerializeField] public int number;

}

// Extension for List
public static class CollectionExtension
{
    private static System.Random rng = new System.Random();

    public static T RandomElement<T>(this IList<T> list)
    {
        return list[rng.Next(list.Count)];
    }

    public static T RandomElement<T>(this T[] array)
    {
        return array[rng.Next(array.Length)];
    }
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

public class WaveGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    MonsterPool[] MonsterPool;

    List<GameObject> Pools = new List<GameObject>();

    [SerializeField] private float  _range = 10f; // Range to spawn monster; only spawn when player is near

    private float _currentTimer = 0; // Timer reache a threshold -> increase difficultity level

    [SerializeField] private float InitialSpawnInterval = 10f;

    private GameObject _player ;
    System.Random rand = new System.Random();  

    private void Awake() {
        _player = GameObject.FindGameObjectWithTag("Player");
        
    }


    private void Start()
    {
        foreach (var monstertype in MonsterPool)
        {
            for (int i = 0; i < monstertype.number; i++)
            {
                GameObject monsterCreated = Instantiate(monstertype.monsterPrefab, transform.position, Quaternion.identity);
                monsterCreated.SetActive(false);
                Pools.Add(monsterCreated);
            }
        }

        Pools.Shuffle();

    }


    // Update is called once per frame
    void Update()
    {
        if (_player == null) return;
        if (Vector2.Distance(transform.position, _player.transform.position) > _range) return;
        _currentTimer += Time.deltaTime;
        if (_currentTimer >= InitialSpawnInterval)
        {
            SummonMonster();
            _currentTimer = 0;
        }
    }

    void SummonMonster()
    {
        if (Pools.Count == 0)
        {
            Destroy(gameObject);
            return;
        }
        GameObject monsterCreated = Pools.RandomElement();
        Pools.Remove(monsterCreated);
        monsterCreated.SetActive(true);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
