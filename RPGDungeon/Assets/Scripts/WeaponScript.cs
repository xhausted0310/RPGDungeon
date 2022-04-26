using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    [SerializeField] private GameObject _player;
    
    private Vector3 _pos;
    private bool _isSwing = false;
    private float _degree = 0f;
    private float _weaponY = -0.45f;
    private float _weaponX = 0.3f;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            Attack();
        }
        
    }
    private void FixedUpdate()
    {
        if(_isSwing)
        {
            _degree -= 7;
            if(_degree < -65)
            {
                _degree = 0;
                _isSwing = false;
                GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);

            }
            transform.eulerAngles = Vector3.forward * _degree;
           

        }
    }

    private void Attack()
    {
        if(_player.GetComponent<PlayerMovement>().turnedLeft)
        {
            transform.localScale = new Vector3(-1.8f, 1.8f, 1);
            _weaponX = -0.3f;
        }
        else
        {
            transform.localScale = new Vector3(1.8f, 1.8f, 1);
            _weaponX = 0.3f;
        }

        _pos = _player.transform.position;
        _pos.x += _weaponX;
        _pos.y += _weaponY;
        transform.position = _pos;

        _isSwing = true;
    }
}
