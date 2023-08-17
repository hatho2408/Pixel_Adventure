using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance ;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private int bgmToPlay;
  


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(!bgm[bgmToPlay].isPlaying)
        {
            bgm[bgmToPlay].Play();
        }
    }
    public void PlaySFX(int sfxPLay)
    {
        if(sfxPLay<sfx.Length)
        {
            sfx[sfxPLay].pitch=Random.Range(0.85f,1.15f);
            sfx[sfxPLay].Play();
        }
        
    }
    public void StopSFX(int sfxStop)
    {
        sfx[sfxStop].Stop();
    }
    public void PlayBGM(int bgmPlay)
    {
        for(int i=0; i<bgm.Length;i++)
        {
            bgm[i].Stop();
        }
        bgm[bgmPlay].Play();
    }
    public void PlayRandomBGM()
    {
        int rand=Random.Range(0,bgm.Length);
        bgmToPlay=rand;
        PlayBGM(rand);
    }
}
