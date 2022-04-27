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

    private float _timer;
    private int _spawnIndex = 0;
    private bool _isGateway = false;
    
    

    void Start()
    {
        //randomly choosing a statue sprite
        int rnd = Random.Range(0, _sprites.Length);
        GetComponent<SpriteRenderer>().sprite = _sprites[rnd];

        Instantiate(_enemyPrefab, _spawnPoints[0].transform.position, Quaternion.identity);
        Instantiate(_enemyPrefab, _spawnPoints[1].transform.position, Quaternion.identity);
        _timer = Time.time + 7.0f;
    }


    void Update()
    {
        //spawning an enemies
        if(_timer<Time.time)
        {
            Instantiate(_enemyPrefab,
                        _spawnPoints[_spawnIndex % 2].transform.position,
                        Quaternion.identity);
            _timer = Time.time + 7.0f;
            _spawnIndex++;
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        GetComponent<SpriteRenderer>().color = Color.red;
        if (_health<=0)
        {
            _isGateway = true;
            _player.GetComponent<PlayerMovement>().GainExperience(20);
            GetComponent<SpriteRenderer>().sprite = _deathSprite;
            if(_isGateway)
            {
                 Invoke("OpenGateway", 0.5f);
            }
            //else
            //{
            //    Destroy(gameObject);
            //}
        }
            Invoke("DefaultColor", 0.3f);
    }

    private void OpenGateway()
    {
        GetComponent<SpriteRenderer>().sprite = _geteway;
    }

    //go back to default color after hit a statue
    private void DefaultColor()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
