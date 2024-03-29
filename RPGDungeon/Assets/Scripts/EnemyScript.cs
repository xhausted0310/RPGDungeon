using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _speed;
    [SerializeField] private int _health = 50;
    [SerializeField] private Sprite _deathSprite;


    private GameManager _gameManager;
    private Transform _target;
    private float _minDistance = 5.0f;
    private float _thrust = 1.5f;
    private bool _isCollide = false;
    private bool _isDead = false;
    private float _enemyDamage = 10f;

    public Sprite[] sprites;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        int rnd = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[rnd];
        _target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        _range = Vector2.Distance(transform.position, _target.position);
        if(_range<_minDistance && !_isDead)
        {
            if(!_isCollide)
            {
                //get position of the player
                transform.LookAt(_target.position);
                //correct the rotation
                transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                transform.Translate(new Vector3(_speed * Time.deltaTime, 0, 0));
            }
        }
        transform.rotation = Quaternion.identity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_isCollide)
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 center = collision.collider.bounds.center;

            _isCollide = true;

            bool right = contactPoint.x > center.x;
            bool left = contactPoint.x < center.x;
            bool top = contactPoint.y > center.y;
            bool bottom = contactPoint.y < center.y;

            if (right)
                GetComponent<Rigidbody2D>().AddForce(transform.right * _thrust, ForceMode2D.Impulse);
            if (left)
                GetComponent<Rigidbody2D>().AddForce(-transform.right * _thrust, ForceMode2D.Impulse);
            if (top)
                GetComponent<Rigidbody2D>().AddForce(transform.up * _thrust, ForceMode2D.Impulse);
            if (bottom)
                GetComponent<Rigidbody2D>().AddForce(-transform.up * _thrust, ForceMode2D.Impulse);
            Invoke("FalseCollision", 0.25f);
        }

    }

    void FalseCollision()
    {
        _isCollide = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        transform.GetChild(0).gameObject.SetActive(true);
        Invoke("HideBlood", 0.25f);
        if (_health<=0)
        {
            _isDead = true;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<SpriteRenderer>().sprite = _deathSprite;
            GetComponent<SpriteRenderer>().sortingOrder = -1;
            GetComponent<Collider2D>().enabled = false;
            _target.GetComponent<PlayerMovement>().GainExperience(5);
            Invoke("EnemyDeath", 1.0f);

        }
    }
    private void HideBlood()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void EnemyDeath()
    {
        _gameManager.SetZombieCount(-1);
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public float GetEnemyDamage()
    {
        return _enemyDamage;
    }
}
