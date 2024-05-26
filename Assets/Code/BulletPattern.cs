[System.Serializable]
public struct BullletPattern
{
    public enum SpawnerType
    {
        Straight,
        Spin,
        Circle,
        Spread,
        Flower,
        Follow
    }
    public enum ProjectileType
    {
        Straight,
        Curved,
        Spiral,
        Wavy
    }

    public SpawnerType _spawnerType;
    public ProjectileType _projectileType;

    // Global params
    public float _speed;

    // Burst params
    public int _bulletsPerShot;
    public float _anglePerBullet;
    public int _burstsPerShot;
    public float _angleOffsetPerBurst;

    // Cone params
    public float _spreadAngle;
    public float _timeBetweenSpreadShots;

    public SpawnerType GetSpawnerType()
    {
        return _spawnerType;
    }
    public ProjectileType GetProjectileType()
    {
        return _projectileType;
    }

    public float GetSpeed()
    {
        return _speed;
    }
    public int GetBulletsPerShot()
    {
        return _bulletsPerShot;
    }
    public float GetAnglePerBullet()
    {
        return _anglePerBullet;
    }
    public float GetBurstsPerShot()
    {
        return _burstsPerShot;
    }
    public float GetAngleOffsetPerBurst()
    {
        return _angleOffsetPerBurst;
    }

    public float GetSpreadAngle()
    {
        return _spreadAngle;
    }
    public float GetTimeBetweenSpreadShots()
    {
        return _timeBetweenSpreadShots;
    }
}
