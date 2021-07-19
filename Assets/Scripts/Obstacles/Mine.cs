using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] float verticalDistance;
    [SerializeField] float horizontalDistance;

    [Range(0, 1)]
    [SerializeField] float moveSpeed;

    [SerializeField] int hp;
    [SerializeField] int damage;
    [SerializeField] GameObject go_EffectPrefab;

    Vector3 endPos1;
    Vector3 endPos2;
    Vector3 currentDestination;

    void Start()
    {
        Vector3 originPos = transform.position;
        endPos1.Set(originPos.x, originPos.y + verticalDistance, originPos.z + horizontalDistance);
        endPos2.Set(originPos.x, originPos.y - verticalDistance, originPos.z - horizontalDistance);
        currentDestination = endPos1;
    }

    void Update()
    {
        if ((transform.position - endPos1).sqrMagnitude <= .1f)
            currentDestination = endPos2;
        else if ((transform.position - endPos2).sqrMagnitude <= .1f)
            currentDestination = endPos1;

        transform.position = Vector3.Lerp(transform.position, currentDestination, moveSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.name == "Player")
            other.transform.GetComponent<StatusManager>().DecreaseHp(damage);
    }

    public void Damaged(int _num)
    {
        hp -= _num;
        if (hp <= 0)
            explosion();
    }

    void explosion()
    {
        SoundManager.instance.PlaySE("MineBurst", .15f);
        GameObject clone = Instantiate(go_EffectPrefab, transform.position, Quaternion.identity);

        Destroy(clone, 2f);
        Destroy(gameObject);
    }
}
