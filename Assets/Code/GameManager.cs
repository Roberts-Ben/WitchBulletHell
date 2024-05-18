using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> bulletPool;

    public GameObject bulletPrefab;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void CreateBullet(BulletSpawner spawner)
    {
        GameObject bullet = null;
        foreach (GameObject go in bulletPool)
        {
            if(!go.activeSelf)
            {
                bullet = go;
                go.SetActive(true);
                break;
            }
        }

        if(bullet == null)
        {
            bullet = Instantiate(bulletPrefab, spawner.transform.position, Quaternion.identity);
            bulletPool.Add(bullet);
        }
        
        bullet.GetComponent<Bullet>().speed = spawner.speed;
        bullet.transform.SetPositionAndRotation(spawner.transform.position, spawner.transform.rotation);
    }

    public GameObject GetBullet(GameObject bullet)
    {
        
        if (bulletPool.Contains(bullet))
        {
            int index = bulletPool.IndexOf(bullet);
            return bulletPool[index];
        }
        else
        {

        }
        return null;
    }

    public void DeactivateBullet(GameObject bullet)
    {
        GetBullet(bullet).SetActive(false);
    }
}
