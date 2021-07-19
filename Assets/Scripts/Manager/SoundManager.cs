using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    [Header("사운드 등록")]
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] Sound[] sfxSounds;

    [Header("브금 플레이어")]
    [SerializeField] AudioSource bgmPlayer;

    [Header("효과음 플레이어")]
    [SerializeField] AudioSource[] sfxPlayer;

    // Use this for initialization
    void Start () {
        instance = this;
        PlayRandomBGM();
    }
	
    public void PlaySE(string _soundName, float _volumn)
    {
        for(int i = 0; i < sfxSounds.Length; i++)
        {
            if(_soundName == sfxSounds[i].soundName)
            {
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    if (!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfxSounds[i].clip;
                        sfxPlayer[x].volume = _volumn;
                        sfxPlayer[x].Play();
                        return;
                    }
                }

                Debug.Log("효과음 플레이어 모두 사용 중");
                return;
            }
        }
    }

    public void PlayRandomBGM()
    {
        int random = Random.Range(0, bgmSounds.Length);
        bgmPlayer.volume = .3f;
        bgmPlayer.clip = bgmSounds[random].clip;
        bgmPlayer.Play();
    }
}
