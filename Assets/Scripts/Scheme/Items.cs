using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Score,
    Bullet,
}

public class Items : MonoBehaviour {

    public ItemType itemType;

    public int extraScore;
    public int extraBullet;

    void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
    }
}
