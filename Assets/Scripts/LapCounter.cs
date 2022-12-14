using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{

	[SerializeField] private MapManager mapManager;
	public Text lapText;
	public Text timerText;
	public GameObject endScreen;
	public float lapTime = 0f;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        lapTime += Time.deltaTime;

        if (mapManager.laps != mapManager.lapAmount + 1)
		{
            endScreen.SetActive(false);
            if (mapManager.laps == 0)
			{
				lapText.text = "Start Race";
			}
			lapText.text = "Lap " + mapManager.laps.ToString();
			timerText.text = lapTime.ToString("F2");
		}
		else
		{
			lapText.text = "Race Over";
			endScreen.SetActive(true);
        }
	}

	void StartTimer()
	{
		lapTime += Time.deltaTime;
	}
}
