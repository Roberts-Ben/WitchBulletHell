using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;
    public float rotateSpeed = 1f;

    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;

    private float timer = 0f;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin)
        {
            transform.Rotate(Vector3.right, rotateSpeed);
        }

        if (timer >= firingRate)
        {
            gameManager.CreateBullet(this);
            timer = 0;
        }
    }
}
