using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed = 0;
    
    private float _horizontal;
    private float _vertical;

    private Rigidbody2D _rb;
    private Animator _animator;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        _rb.velocity = new Vector2(_horizontal * _speed, _vertical * _speed);

        if(_horizontal>0)
        {
            _animator.Play("Right");
        }if(_horizontal<0)
        {
            _animator.Play("Left");
        }
        if (_vertical > 0)
        {
            _animator.Play("Up");
        }
        if (_vertical < 0)
        {
            _animator.Play("Down");
        }
    }
}
