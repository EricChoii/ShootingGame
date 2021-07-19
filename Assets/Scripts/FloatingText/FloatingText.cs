using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour {

    [SerializeField] Animation anim;
    [SerializeField] float destoryTime;

    // Use this for initialization
    void Start () {
        anim.Play();
        Destroy(gameObject, destoryTime);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
