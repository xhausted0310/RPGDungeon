using UnityEngine;

public class WeaponColliderScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(10);
        }
    }
}
