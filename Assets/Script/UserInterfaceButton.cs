using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceButton : MonoBehaviour
{
    public GameObject InfoPanel_CPU;
    public GameObject InfoPanel_RAM;

    private bool showInfoCpu = false;
    private bool showInfoRam = false;

    // Start is called before the first frame update
    void Start()
    {
     InfoPanel_CPU.SetActive(false);
     InfoPanel_RAM.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //CPU info panel
    public void ShowHideInfoCPU()
    {
        if(!showInfoCpu)
        {
            InfoPanel_CPU.SetActive(true);
            showInfoCpu = true;
        }
        else
        {
            InfoPanel_CPU.SetActive(false);
            showInfoCpu = false;
        }
    }

    //RAM info panel
    public void ShowHideInfoRAM()
    {
        if(!showInfoRam)
        {
            InfoPanel_RAM.SetActive(true);
            showInfoRam = true;
        }
        else
        {
            InfoPanel_RAM.SetActive(false);
            showInfoRam = false;
        }
    }


    //7 may added - Open URL Google form button
    public void OpenURL()
    {
        Application.OpenURL("https://forms.gle/MUqPF4fgfM2Aeyia9");
        Debug.Log("is this working??? made for questionnaire huehuehuehue");
    }

    public void ChangeScene (string a)
	{
		SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Click);
		Application.LoadLevel (a);
	}

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
