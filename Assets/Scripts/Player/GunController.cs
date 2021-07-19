using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour {

    [Header("현재 장착 총")]
    public Gun currentGun;

    [Header("쿨타임 이미지")]
    [SerializeField] Image img_Skill;

    [SerializeField] Text txt_CurrentGunBullet;

    private float nextFire;

    // Use this for initialization
    void Start () {
        img_Skill.gameObject.SetActive(false);
        BulletUISetting();
    }
	
    public void BulletUISetting()
    {
        txt_CurrentGunBullet.text = "x " + currentGun.bulletCnt;
    }

    // Update is called once per frame
    void Update () {
        TryFire();
        LockOnMouse();
	}

    void LockOnMouse()
    {
        Vector3 camPos = Camera.main.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camPos.z));
        Vector3 target = new Vector3(0f, mousePos.y, mousePos.z);

        transform.LookAt(target);
    }

    void Fire()
    {
        currentGun.bulletCnt--;
        currentGun.animator.SetTrigger("GunFire");
        SoundManager.instance.PlaySE("NormalGun_Fire", .05f);
        StartCoroutine(CoolTime(currentGun.fireRate+1));
        StartCoroutine(CreateBullet());
    }

    IEnumerator CreateBullet()
    {
        currentGun.ps_MuzzleFlash.Play();
        BulletUISetting();

        Transform _parent = GameObject.Find("TobeRemoved").GetComponent<Transform>();
        var clone = Instantiate(currentGun.go_Bullet_Prefab, currentGun.ps_MuzzleFlash.transform.position, Quaternion.identity);
        clone.transform.parent = _parent;
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * currentGun.speed);

        yield return new WaitForFixedUpdate();
    }

    void TryFire()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            if (currentGun.bulletCnt > 0)
            {
                img_Skill.gameObject.SetActive(true);
                nextFire = Time.time + currentGun.fireRate;
                Fire();
            }
            else
            {
                nextFire = Time.time + currentGun.fireRate;
                SoundManager.instance.PlaySE("dryFire", .3f);
            }
        }
    }

    IEnumerator CoolTime(float cool)
    {
        while (cool > 0)
        {
            cool -= Time.deltaTime;
            img_Skill.fillAmount = (1.0f / cool);
            yield return new WaitForFixedUpdate();
        }
    }
}
