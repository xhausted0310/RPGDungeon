using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawners;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _hudCanvas;


    private int _level = 1;
    private int _zombieCount = 0;
    private int _zombieLimit = 10;

    void Start()
    {
        _spawners = GameObject.FindGameObjectsWithTag("Spawner");
        int rnd = Random.Range(0, _spawners.Length);
        _spawners[rnd].GetComponent<SpawnerScript>().SetGeteway(true);
        foreach(GameObject spawner in _spawners)
        {
            spawner.GetComponent<SpawnerScript>().SetHealth(_level + Random.Range(30, 60));
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(_player.gameObject);
        DontDestroyOnLoad(_weapon.gameObject);
        DontDestroyOnLoad(_hudCanvas.gameObject);
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

    public void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
