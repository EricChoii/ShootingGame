using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [Header("피격 이펙트")]
    [SerializeField] GameObject go_RecochetEffect;

    [Header("총알 데미지")]
    [SerializeField] int damage;

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.name != "Bullet(Clone)")
        {
            ContactPoint contactPoint = other.contacts[0];

            SoundManager.instance.PlaySE("NormalGun_Recochet", .05f);

            Transform _parent = GameObject.Find("TobeRemoved").GetComponent<Transform>();
            var clone = Instantiate(go_RecochetEffect, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));
            clone.transform.parent = _parent;

            if (other.transform.CompareTag("Mine"))
                other.transform.GetComponent<Mine>().Damaged(damage);
            
            Destroy(clone, 0.5f);
            Destroy(gameObject);
        }
    }
}
