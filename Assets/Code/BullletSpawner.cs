using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BullletPattern;

public class BulletSpawner : MonoBehaviour
{
    public List<BullletPattern> pattern;

    public GameObject bullet;
    public float bulletLife = 1f;

    private float timer = 0f;
    private int currentPattern = 0;

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
            switch (pattern[currentPattern]._spawnerType)
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
                case SpawnerType.Follow:
                    StartCoroutine(FireFollow());
                    break;
                default:
                    break;
            }
            timer = 0;

            if(pattern.Count > 1)
            {
                if (currentPattern < pattern.Count - 1)
                {
                    currentPattern++;
                }
                else
                {
                    currentPattern = 0;
                }
            }
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
        rotation += pattern[currentPattern].GetAnglePerBullet();
        yield return null;
    }
    IEnumerator FireCircle()
    {
        yield return new WaitForSeconds(firingRate);
        rotation += pattern[currentPattern].GetAngleOffsetPerBurst();
        for (int i = 0; i < pattern[currentPattern].GetBulletsPerShot(); i++)
        {
            gameManager.CreateBullet(this, Quaternion.AngleAxis(rotation, transform.right));
            rotation += pattern[currentPattern].GetAnglePerBullet();
        }

        yield return null;
    }
    IEnumerator FireSpread()
    {
        rotation = -pattern[currentPattern].GetSpreadAngle() * 1.5f;
        
        for (int i = 0; i < pattern[currentPattern].GetBulletsPerShot(); i++)
        {
            yield return new WaitForSeconds(pattern[currentPattern].GetTimeBetweenSpreadShots());
            gameManager.CreateBullet(this, Quaternion.AngleAxis(rotation, transform.right));
            float angleDelta = pattern[currentPattern].GetSpreadAngle() / pattern[currentPattern].GetBulletsPerShot();
            rotation -= angleDelta;
        }
        yield return null;
    }
    IEnumerator FireFollow()
    {
        gameManager.CreateBullet(this, Quaternion.LookRotation(gameManager.GetPlayerGameObject().transform.position,transform.up));
        yield return null;
    }
}
