using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable] // show custom class in inspector
public class Sound
{
	public string name;
	public AudioClip clip;
	[Range(0f, 1f)]
	public float volume;
	[Range(.1f, 3f)]
	public float pitch;
	public Boolean loop;
	[HideInInspector]
	public AudioSource source;
}
