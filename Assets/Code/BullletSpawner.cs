using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin , Circle, Spread, Flower, Ring, Follow}
    enum ProjetileType {Straight, Curved, Spiral, Wavy }

    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;

    // Circular pattern
    public int bulletsPerShot = 1;
    public float anglePerBullet = 1f;

    // Ring Pattern
    public int burstsPerShot = 1;
    public float angleOffsetPerBurst = 1f;

    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private ProjetileType projectileType;
    [SerializeField] private float firingRate = 1f;

    private float rotation = 0;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        switch (spawnerType)
        {
            case SpawnerType.Straight:
                InvokeRepeating("FireStraight", 0f, firingRate);
                break;
            case SpawnerType.Spin:
                InvokeRepeating("FireSpin", 0f, firingRate);
                break;
            case SpawnerType.Circle:
                InvokeRepeating("FireCircle", 0f, firingRate);
                break;
            case SpawnerType.Spread:
                break;
            case SpawnerType.Flower:
                break;
            case SpawnerType.Ring:
                for(int i = 0; i < burstsPerShot; i++)
                {
                    InvokeRepeating("FireRing", 0f, firingRate);
                }
                break;
            case SpawnerType.Follow:
                transform.LookAt(gameManager.GetPlayerGameObject().transform.position); // FIX
                InvokeRepeating("FireFollow", 0f, firingRate);
                break;
            default:
                break;
        } 
    }


    private void FireStraight()
    {
        gameManager.CreateBullet(this, transform.rotation);
    }
    private void FireSpin()
    {
        rotation += anglePerBullet;
        gameManager.CreateBullet(this, Quaternion.AngleAxis(rotation, transform.right));
    }
    private void FireCircle()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            gameManager.CreateBullet(this, Quaternion.AngleAxis(anglePerBullet * i, transform.right));
        }
    }
    private void FireRing()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Debug.Log(Quaternion.AngleAxis(anglePerBullet * i, transform.right));
            gameManager.CreateBullet(this, Quaternion.AngleAxis(anglePerBullet * i, transform.right));
        }
    }
    private void FireFollow()
    {
        gameManager.CreateBullet(this, Quaternion.identity);
    }
    
}
