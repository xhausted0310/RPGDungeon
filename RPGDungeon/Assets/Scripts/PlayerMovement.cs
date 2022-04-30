using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private Image _healthFill;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Text _expText;
    [SerializeField] private float _speed = 0;
    [SerializeField] private float _health = 100;
    [SerializeField] private float _startHealth;
    [SerializeField] private int _exp = 0;
    
    private float _horizontal;
    private float _vertical;
    private float _healthWidth;
    

    private Rigidbody2D _rb;
    private Animator _animator;
    

    public bool turnedLeft = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _healthWidth = _healthFill.sprite.rect.width;
        _startHealth = _health;
    }

    
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _rb.velocity = new Vector2(_horizontal * _speed, _vertical * _speed);
        turnedLeft = false;
        if(_horizontal>0)
        {
            _animator.Play("Right");
        }else if(_horizontal<0)
        {
            _animator.Play("Left");
            turnedLeft = true;
        }else if (_vertical > 0)
        {
            _animator.Play("Up");
        }else if (_vertical < 0)
        {
            _animator.Play("Down");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            transform.GetChild(0).gameObject.SetActive(true);

            _health -= collision.gameObject.GetComponent<EnemyScript>().GetEnemyDamage();
            Vector2 temp = new Vector2(_healthWidth * (_health/_startHealth), _healthFill.sprite.rect.height);
            _healthFill.rectTransform.sizeDelta = temp;

            Invoke("HidePlayerBlood", 0.25f);
            if(_health <= 0)
            {
                _healthFill.enabled = false;
                _gameOverText.gameObject.SetActive(true);
                _gameOverText.text = "GAME OVER";
            }
        }
        if (collision.gameObject.CompareTag("Spawner"))
        {
            collision.gameObject.GetComponent<SpawnerScript>().GetGeteway();
        }

    }
    private void HidePlayerBlood()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
    public void GainExperience(int expAmount)
    {
        _exp += expAmount;
        _expText.text = _exp.ToString();
    }
}
