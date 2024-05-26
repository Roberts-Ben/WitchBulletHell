using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float firingRate = 1;

    public float bulletSpeed = 1;
    public float bulletCount = 1;

    public List<GameObject> bulletfirePositions;

    private float timer = 0;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(bulletCount < bulletfirePositions.Count)
            {
                bulletCount++;
            }
        }
        transform.position += moveSpeed * Time.deltaTime * new Vector3(0f, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        timer += Time.deltaTime;
        
        if (timer >= firingRate)
        {
            gameManager.CreatePlayerBullet(this);
            timer = 0;
        }
    }
}
