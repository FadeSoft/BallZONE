using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class Panel : MonoBehaviour
{
	public GameObject GameOverPanel;
    public GameObject Panel1;
	public GameObject parentObj;



	public void ShowHidePanel(bool status)
    {
		Panel1.gameObject.SetActive(status);
    }

	public void SetGameOverPanelActive(bool status) 
	{
		GameOverPanel.SetActive (status);
		Time.timeScale = 0;
	}
	public void PauseResumeGame()
	{
		bool durum = Time.timeScale == 0;
		ShowHidePanel (!durum);
		if (durum)  
		{
			Time.timeScale = 1;
		}
		else
		{
			Time.timeScale = 0;
		}
	}
	
	
	public void RestartGame()
	{		
		SceneManager.LoadScene (1);
		Application.LoadLevel (Application.loadedLevelName);
		Time.timeScale = 1; 
	}
	public void Resume()
	{		
		Panel1.SetActive (false);
		Time.timeScale = 1; 
	}
	public void Menu()
	{
		Time.timeScale = 1; 
		SceneManager.LoadScene (0);
	}
	public void Menu2()
	{
		SceneManager.LoadScene(0);
	}


}
