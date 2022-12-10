using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextCourse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextTrack()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

	public void MainMenu()
	{
        SceneManager.LoadScene(0);
    }
}
