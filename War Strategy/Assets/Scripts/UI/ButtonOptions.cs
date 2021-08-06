using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOptions : MonoBehaviour
{
    private AudioSource _source;

    private void Start()
    {
        _source = GameObject.Find("SOUND MANAGER").GetComponent<AudioSource>();
    }

    public void ClickSound(AudioClip sound)
    {
        _source.PlayOneShot(sound);
    }
}
