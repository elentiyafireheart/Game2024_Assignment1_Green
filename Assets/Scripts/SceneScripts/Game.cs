using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject _bulletPrefab;
    public Transform StarPoint;

  public void Shoot()
    {
        Instantiate(_bulletPrefab, StarPoint.position, transform.rotation);
    }
}
