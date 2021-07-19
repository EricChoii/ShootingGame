using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackController : FollowPlayer {

    [Header("Jet Engine Spin Speed")] [Range(0,1)]
    [SerializeField] float spinSpeed;

	// Use this for initialization
	void Start () {
        curPos = tf_Player.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.position = Vector3.Lerp(transform.position, tf_Player.position - curPos, speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), spinSpeed);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.position = Vector3.Lerp(transform.position, tf_Player.position - new Vector3(curPos.x, curPos.y, -curPos.z), speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-100, 0, 0), spinSpeed);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            transform.position = Vector3.Lerp(transform.position, tf_Player.position - new Vector3(curPos.x, curPos.y, 0), speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-56, 0, 0), spinSpeed);
        }
    }
}
