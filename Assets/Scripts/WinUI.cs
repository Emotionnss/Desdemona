using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour {

	private Animator anim;

	void GameEnd()
	{
		SceneManager.LoadScene(0);
	}

	void Finishplay()
	{
		gameObject.SetActive(false);
	}
}
