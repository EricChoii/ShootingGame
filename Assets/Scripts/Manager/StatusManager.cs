using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{

    [SerializeField] float blinkSpeed;
    [SerializeField] int blinkCount;
    [SerializeField] MeshRenderer mesh_PlayerGraphic;
    [SerializeField] int maxHp;
    int currentHp;

    [SerializeField] Text[] hpArray;

    private bool _IsHurt = false;

    JetEngine_FuelManager theJetEngine;

    public bool IsHurt
    {
        get { return _IsHurt; }
    }

    void Start()
    {
        theJetEngine = FindObjectOfType<JetEngine_FuelManager>();
        currentHp = maxHp;
        UpdateHpStatus();
    }

    public void DecreaseHp(int _num)
    {
        if (!_IsHurt)
        {
            currentHp -= _num;
            UpdateHpStatus();

            if (currentHp <= 0)
            {
                PlayerDead();
                return;
            }

            _IsHurt = true;
            SoundManager.instance.PlaySE("Hurt", .3f);
            theJetEngine.getHit();
            StartCoroutine(BlinkCoroutine());
        }
    }

    public void IncreaseHp(int _num)
    {
        if (currentHp == maxHp)
            return;

        currentHp += _num;
        if (currentHp > maxHp)
            currentHp = maxHp;

        UpdateHpStatus();
    }

    void PlayerDead()
    {
        transform.gameObject.SetActive(false);
        FloatingTextManager.instance.CreateFloatingText(transform.position, "You died");
        print("You died");
    }

    IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < blinkCount * 2; i++)
        {
            mesh_PlayerGraphic.enabled = !mesh_PlayerGraphic.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }

        _IsHurt = false;
    }

    void UpdateHpStatus()
    {
        for (int i = 0; i < hpArray.Length; i++)
        {
            if (i < currentHp)
                hpArray[i].gameObject.SetActive(true);
            else
                hpArray[i].gameObject.SetActive(false);
        }
    }
}
