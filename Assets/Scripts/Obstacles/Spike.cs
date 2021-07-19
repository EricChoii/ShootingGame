using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    [SerializeField] int spikeDamage;
    [SerializeField] float force;

    GameObject currGun;

    Explosion theExplosion;

    int spikeHp = 3;

    void Start()
    {
        currGun = GameObject.FindGameObjectWithTag("HoldingGun");

        theExplosion = transform.GetComponent<Explosion>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (!other.transform.GetComponent<StatusManager>().IsHurt)
            {
                other.transform.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, 5f);
                other.transform.GetComponent<StatusManager>().DecreaseHp(spikeDamage);
            }
        }
        else if (other.transform.CompareTag("Weapon"))
        {
            if (spikeHp > currGun.transform.GetComponent<GunController>().currentGun.damage)
                spikeHp -= currGun.transform.GetComponent<GunController>().currentGun.damage;
            else
            {
                theExplosion.explode(transform.gameObject);
                SoundManager.instance.PlaySE("Broken", .1f);
                Destroy(transform.gameObject);
            }
        }
    }
}
