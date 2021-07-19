using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speed Management")]
    [SerializeField]float moveSpeed;
    [SerializeField] float jetPackSpeed;

    public bool IsJet { get; private set; }

    Rigidbody myRigid;

    [Header("파티클 부스터")]
    [SerializeField] ParticleSystem ps_LeftEngine;
    [SerializeField] ParticleSystem ps_RightEngine;

    AudioSource audioSource;

    JetEngine_FuelManager theFuel;
    StatusManager theStatus;

    // Use this for initialization
    void Start () {
        IsJet = false;
        myRigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        theFuel = FindObjectOfType<JetEngine_FuelManager>();
        theStatus = FindObjectOfType<StatusManager>();
    }
	
	// Update is called once per frame
	void Update () {

        TryMove();
        TryJet();
  
	}

    void TryJet()
    {
        if (Input.GetKey(KeyCode.Space) && theFuel.IsFuel && !theStatus.IsHurt)
        {
            if (!IsJet)
            {
                ps_LeftEngine.Play();
                ps_RightEngine.Play();
                audioSource.Play();
                IsJet = true;
            }

            myRigid.AddForce(Vector3.up * jetPackSpeed);
        }
        else
        {
            if (IsJet)
            {
                ps_LeftEngine.Stop();
                ps_RightEngine.Stop();
                audioSource.Stop();
                IsJet = false;
            }

            myRigid.AddForce(Vector3.down * jetPackSpeed);
        }
    }
    
    void TryMove()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 moveDir = new Vector3(0, 0, Input.GetAxisRaw("Horizontal"));
            myRigid.AddForce(moveDir * moveSpeed);
        }
    }
}
