﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Header("따라갈 플레이어 설정")]
    [SerializeField] Transform tf_Player;

    [Header("따라갈 스피드 설정")] [Range(0, 1f)]
    [SerializeField] float chaseSpeed;

    float camNormalXPos;
    [Header("부스터시 떨어질 x거리")]
    [SerializeField] float camJetXPos;
    float camCurrentXPos;

    Player thePlayer;

    // Use this for initialization
    void Start () {
        thePlayer = tf_Player.GetComponent<Player>();
        camNormalXPos = transform.position.x;
        camCurrentXPos = camNormalXPos;
    }
	
	// Update is called once per frame
	void Update () {
        if (thePlayer.IsJet)
            camCurrentXPos = camJetXPos;
        else
            camCurrentXPos = camNormalXPos;

        Vector3 movePos = Vector3.Lerp(transform.position, tf_Player.position, chaseSpeed);
        float cameraPosX = Mathf.Lerp(transform.position.x, camCurrentXPos, chaseSpeed);
        transform.position = new Vector3(cameraPosX, movePos.y, movePos.z);
	}
}
