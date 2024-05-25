using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> bulletPool;
    public List<GameObject> playerBulletPool;

    public GameObject bulletPrefab;
    public GameObject playerBulletPrefab;

    public static GameObject player;

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
        player = GameObject.Find("Player");
    }

    public GameObject GetPlayerGameObject()
    {
        return player;
    }

    public void CreateBullet(BulletSpawner spawner, Quaternion _rotation)
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
            bullet = Instantiate(bulletPrefab, spawner.transform.position, _rotation);
            bulletPool.Add(bullet);
        }
        
        bullet.GetComponent<Bullet>().speed = spawner.speed;
        bullet.transform.SetPositionAndRotation(spawner.transform.position, _rotation);
    }

    public void CreatePlayerBullet(CharacterController character)
    {
        for(int i = 0; i < character.bulletCount; i++)
        {
            GameObject bullet = null;
            foreach (GameObject go in playerBulletPool)
            {
                if (!go.activeSelf)
                {
                    bullet = go;
                    go.SetActive(true);
                    break;
                }
            }

            if (bullet == null)
            {
                bullet = Instantiate(playerBulletPrefab, character.bulletfirePositions[i].transform.position, Quaternion.identity);
                playerBulletPool.Add(bullet);
            }

            bullet.GetComponent<PlayerBullet>().speed = character.bulletSpeed;
            bullet.transform.SetPositionAndRotation(character.bulletfirePositions[i].transform.position, character.transform.rotation);
        }
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
