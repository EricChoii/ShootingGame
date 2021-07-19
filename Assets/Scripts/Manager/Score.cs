using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    int currentScore;
    public static int extraScore; // 아이템 점수
    int distanceScore;
    float maxDistance;
    float originPosZ;

    [SerializeField] Text txt_Score;
    [SerializeField] Transform tf_Player;


    // Use this for initialization
    void Start () {
        originPosZ = tf_Player.position.z;

    }
	
	// Update is called once per frame
	void Update () {
        if(tf_Player.position.z > maxDistance)
        {
            maxDistance = tf_Player.position.z;
            distanceScore = Mathf.RoundToInt(maxDistance - originPosZ);
        }

        currentScore = extraScore + distanceScore;
        txt_Score.text = string.Format("{0:000,000}", currentScore);
	}
}
