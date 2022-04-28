using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _spawnPoints;
    [SerializeField] private float _health = 50f;
    [SerializeField] private Sprite _deathSprite;
    [SerializeField] private Sprite _geteway;
    [SerializeField] private Sprite[] _sprites;

    private GameManager _gameManager;
    private float _timer;
    private int _spawnIndex = 0;
    private bool _isGateway = false;
    
    
    

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //randomly choosing a statue sprite
        int rnd = Random.Range(0, _sprites.Length);
        GetComponent<SpriteRenderer>().sprite = _sprites[rnd];

        Instantiate(_enemyPrefab, _spawnPoints[0].transform.position, Quaternion.identity);
        Instantiate(_enemyPrefab, _spawnPoints[1].transform.position, Quaternion.identity);
        _timer = Time.time + 7.0f;
        _gameManager.SetZombieCount(2);
    }


    void Update()
    {
        //spawning an enemies
        if(_timer<Time.time && _gameManager.GetZombieCount() < _gameManager.GetZombieLimit())
        {
            Instantiate(_enemyPrefab,
                        _spawnPoints[_spawnIndex % 2].transform.position,
                        Quaternion.identity);
            _timer = Time.time + 7.0f;
            _spawnIndex++;
            _gameManager.SetZombieCount(1);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        GetComponent<SpriteRenderer>().color = Color.red;
        if (_health<=0)
        {           
            GetComponent<SpriteRenderer>().sprite = _deathSprite;
            if(_isGateway)
            {
                Invoke("OpenGateway", 0.5f);
            }
            else
            {
                Invoke("DestroyGateway", 0.6f);
            }
        }
            Invoke("DefaultColor", 0.3f);
    }

    private void OpenGateway()
    {
        _player.GetComponent<PlayerMovement>().GainExperience(20);
        GetComponent<SpriteRenderer>().sprite = _geteway;
    }

    private void DestroyGateway()
    {
        Destroy(gameObject);
    }

    //go back to default color after hit a statue
    private void DefaultColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SetHealth(int newHealth)
    {
        _health = newHealth;
    }

    public void SetGeteway(bool isOpen)
    {
        _isGateway = isOpen;
    }

}
