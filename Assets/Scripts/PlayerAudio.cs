using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public float volume = 0.5f;
    public AudioClip grab_01;
    public AudioClip grab_02;
    public AudioClip grab_03;
    public AudioClip grab_04;
    public AudioClip grab_05;
    public List<AudioClip> clipList;

    public void Start()
    {
        clipList = new List<AudioClip>();
        clipList.Add(grab_01);
        clipList.Add(grab_02);
        clipList.Add(grab_03);
        clipList.Add(grab_04);
        clipList.Add(grab_05);
    }

    public void GrabAudioEvent()
    {
        AudioSource.PlayClipAtPoint(clipList[Random.Range(0, clipList.Count)], transform.position, volume);
    }
}
