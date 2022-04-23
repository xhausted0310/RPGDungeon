using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _target;

    private float _minDistance = 5.0f;
    private float _thrust = 2.0f;
    private bool _isCollide = false;

    void Update()
    {
        _range = Vector2.Distance(transform.position, _target.position);
        if(_range<_minDistance)
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
        if(collision.gameObject.CompareTag("Player"))
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
}
