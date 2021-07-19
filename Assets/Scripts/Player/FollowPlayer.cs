using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    [Header("Chasing Speed")]
    [Range(0, 1)]
    [SerializeField] protected float speed;

    [Header("Chasing Object")]
    [SerializeField] protected Transform tf_Player;

    protected Vector3 curPos;

	// Use this for initialization
	void Start () {
        curPos = tf_Player.position - transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, tf_Player.position - curPos, speed);
	}
}
