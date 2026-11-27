using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class Volume : MonoBehaviour
{
public AudioMixer Mixer;

public TextMeshProUGUI Vol;

private float SavedVolume;

    public void Start()
    {
		if(PlayerPrefs.HasKey(transform.name))
		{
			SavedVolume = PlayerPrefs.GetFloat(transform.name);
			GetComponentInChildren<Slider>().value = SavedVolume;
			Mixer.SetFloat(transform.name, Mathf.Log10(SavedVolume) * 20);
			Vol.text = Mathf.FloorToInt(SavedVolume * 100f).ToString();
			return;
		}
		SavedVolume = 1;
    }

	public void SetVol(float level)
	{
		Mixer.SetFloat(transform.name, Mathf.Log10(level) * 20);
		Vol.text = Mathf.FloorToInt(level * 100f).ToString();
		PlayerPrefs.SetFloat(transform.name, level);
	}
}
