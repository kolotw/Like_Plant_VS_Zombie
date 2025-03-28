using UnityEngine;

public class ArrorController : MonoBehaviour
{
    public float speed = 8f;
    

    private void FixedUpdate()
    {
        this.transform.position += Vector3.right * Time.fixedDeltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyController>().getDamage(20);
            this.gameObject.SetActive(false);

        }
        
    }

}
