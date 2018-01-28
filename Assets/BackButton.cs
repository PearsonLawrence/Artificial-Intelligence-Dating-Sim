using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
	public void ChangeLevel(string levelName)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
	}
}
