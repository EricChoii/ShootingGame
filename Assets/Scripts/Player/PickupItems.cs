using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : MonoBehaviour
{
    const int NORMAL_GUN = 0;

    [SerializeField] Gun[] guns;
    GunController theGC;

    void Start()
    {
        theGC = FindObjectOfType<GunController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Items"))
        {
            Items item = other.GetComponent<Items>();

            int extra = 0;

            if (item.itemType == ItemType.Score)
            {
                SoundManager.instance.PlaySE("PickupItem", .4f);
                extra = item.extraScore;
                Score.extraScore += extra;
            }
            else if (item.itemType == ItemType.Bullet)
            {
                SoundManager.instance.PlaySE("Reload", .7f);
                extra = item.extraBullet;
                guns[NORMAL_GUN].bulletCnt += extra;
                theGC.BulletUISetting();
            }

            string msg = "+" + extra;
            FloatingTextManager.instance.CreateFloatingText(other.transform.position, msg);
            Destroy(other.gameObject);
        }
    }
}
