using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameManager gameManager;

    public float speed = 1f;


    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("KillZone"))
        {
            gameManager.DeactivateBullet(this.gameObject);
        }
        if (other.CompareTag("Player"))
        {
            gameManager.DeactivateBullet(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
