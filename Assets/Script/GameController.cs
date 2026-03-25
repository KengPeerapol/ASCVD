using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 CheckpointPos;
    Rigidbody2D playerRb;
    

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        CheckpointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        CheckpointPos = pos;
    }

    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    IEnumerator Respawn(float duration)
    {
        playerRb.simulated = false;
        playerRb.velocity = new Vector2(0, 0);
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = CheckpointPos;
        transform.localScale = new Vector3(1, 1, 1);
        playerRb.simulated = true;
    }


    
}
