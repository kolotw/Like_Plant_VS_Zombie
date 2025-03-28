using UnityEngine;

public class AttackRangeController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Solider"))
        {
            collision.GetComponent<SoliderController>().getDamage(EnemyController.attack);
            gameObject.SetActive(false);
        }
    }
}
