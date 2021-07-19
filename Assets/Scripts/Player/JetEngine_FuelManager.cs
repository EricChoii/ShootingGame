using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetEngine_FuelManager : MonoBehaviour
{
    [SerializeField] float maxFuel;
    float currentFuel;

    [SerializeField] float waitRechargeFuel; // 재충전 대기 시간
    float currentWaitRechargeFuel; // 계산

    [SerializeField] float rechargeSpeed;
    [SerializeField] float fuelReduction;

    [SerializeField] Slider slider_JetEngine;
    [SerializeField] Text txt_JetEngine;

    [SerializeField] float blinkSpeed;
    [SerializeField] int blinkCount;
    [SerializeField] Image Fill;

    Color MaxFuelColor = Color.green;
    Color MinFuelColor = Color.red;
    public bool IsFuel { get; private set; }

    Player thePC;
    StatusManager theStatus;

    // Use this for initialization
    void Start()
    {
        currentFuel = maxFuel;
        slider_JetEngine.maxValue = maxFuel;
        slider_JetEngine.value = currentFuel;

        thePC = FindObjectOfType<Player>();
        theStatus = FindObjectOfType<StatusManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar(currentFuel);
        CheckFuel();
        UsedFuel();
    }

    public void getHit()
    {
        StartCoroutine(BlinkCoroutine());

        if (currentFuel >= fuelReduction)
        {
            currentFuel -= fuelReduction;
        }
        else
            currentFuel = 0;
    }

    void UsedFuel()
    {
        if (thePC.IsJet)
        {
            slider_JetEngine.gameObject.SetActive(true);

            if (currentFuel > 0)
            {
                currentFuel -= Time.deltaTime;
                currentWaitRechargeFuel = 0;
            }
        }
        else
        {
            if (!theStatus.IsHurt)
                FuelRecharge();
        }

        txt_JetEngine.text = Mathf.Round(currentFuel / maxFuel * 100f).ToString() + "%";
        slider_JetEngine.value = currentFuel;
    }

    void CheckFuel()
    {
        if (currentFuel > 0)
            IsFuel = true;
        else
            IsFuel = false;
    }

    void FuelRecharge()
    {
        if (currentFuel < maxFuel)
        {
            currentWaitRechargeFuel += Time.deltaTime;
            if (currentWaitRechargeFuel >= waitRechargeFuel)
                currentFuel += rechargeSpeed * Time.deltaTime;
        }
        else
            slider_JetEngine.gameObject.SetActive(false);
    }

    IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < blinkCount * 2; i++)
        {
            Fill.enabled = !Fill.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
    }

    public void UpdateHealthBar(float val)
    {
        slider_JetEngine.value = val;
        Fill.color = Color.Lerp(MinFuelColor, MaxFuelColor, (float)val / maxFuel);
    }
}
