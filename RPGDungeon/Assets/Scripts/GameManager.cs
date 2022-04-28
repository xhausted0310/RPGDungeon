using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawners;


    private int _level = 1;
    private int _zombieCount = 0;
    private int _zombieLimit = 10;

    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        int rnd = Random.Range(0, spawners.Length);
        spawners[rnd].GetComponent<SpawnerScript>().SetGeteway(true);
        foreach(GameObject spawner in spawners)
        {
            spawner.GetComponent<SpawnerScript>().SetHealth(_level + Random.Range(30, 60));
        }
    }

    public void SetZombieCount(int count)
    {
        _zombieCount += count;
    }

    public int GetZombieCount()
    {
        return _zombieCount;
    }

    public int GetZombieLimit()
    {
        return _zombieLimit;
    }
}
