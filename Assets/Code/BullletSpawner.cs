using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin , Circle, Spread, Flower, Ring, Follow, Cone}
    enum ProjetileType {Straight, Curved, Spiral, Wavy }

    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;

    private float timer = 0f;

    // Circular pattern
    public int bulletsPerShot = 1;
    public float anglePerBullet = 1f;

    // Spread Pattern
    public float spreadAngle = 1f;
    public float timeBetweenSpreadShots = 1f;

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
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= firingRate)
        {
            switch (spawnerType)
            {
                case SpawnerType.Straight:
                    StartCoroutine(FireStraight());
                    break;
                case SpawnerType.Spin:
                    StartCoroutine(FireSpin());
                    break;
                case SpawnerType.Circle:
                    StartCoroutine(FireCircle());
                    break;
                case SpawnerType.Spread:
                    StartCoroutine(FireSpread());
                    break;
                case SpawnerType.Flower:
                    break;
                case SpawnerType.Ring:
                    for(int i = 0; i < burstsPerShot; i++)
                    {
                        StartCoroutine(FireRing(i));
                    }  
                    break;
                case SpawnerType.Follow:
                    StartCoroutine(FireFollow());
                    break;
                case SpawnerType.Cone:
                    StartCoroutine(FireCone());
                    break;
                default:
                    break;
            }
            timer = 0;
        }
    }


    IEnumerator FireStraight()
    {
        gameManager.CreateBullet(this, transform.rotation);
        yield return null;
    }
    IEnumerator FireSpin()
    {
        gameManager.CreateBullet(this, transform.rotation * Quaternion.AngleAxis(rotation, transform.right));
        rotation += anglePerBullet;
        yield return null;
    }
    IEnumerator FireCircle()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            gameManager.CreateBullet(this, Quaternion.AngleAxis(rotation, transform.right));
            rotation += anglePerBullet;
        }
        yield return null;
    }
    IEnumerator FireSpread()
    {
        rotation = spreadAngle * 1.5f;
        
        for (int i = 0; i < bulletsPerShot; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpreadShots);
            gameManager.CreateBullet(this, Quaternion.AngleAxis(rotation, transform.right));
            float angleDelta = spreadAngle / bulletsPerShot;
            rotation += angleDelta;
        }
        yield return null;
    }
    IEnumerator FireRing(int currentBurst)
    {
        yield return new WaitForSeconds(firingRate * currentBurst / 2);
        rotation += angleOffsetPerBurst;
        for (int i = 0; i < bulletsPerShot; i++)
        {
            gameManager.CreateBullet(this, Quaternion.AngleAxis(rotation, transform.right));
            rotation += anglePerBullet;
        }

        yield return null;
    }
    IEnumerator FireFollow()
    {
        gameManager.CreateBullet(this, Quaternion.LookRotation(gameManager.GetPlayerGameObject().transform.position,transform.up));
        yield return null;
    }
    IEnumerator FireCone()
    {
        rotation = spreadAngle / 2;
        float angleDelta = spreadAngle / bulletsPerShot;
        for (int i = 0; i < bulletsPerShot; i++)
        {
            gameManager.CreateBullet(this, Quaternion.AngleAxis(rotation, transform.right));
            rotation += angleDelta;
        }
        yield return null;
    }
}
