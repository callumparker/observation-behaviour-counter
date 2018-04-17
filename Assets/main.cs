using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class main : MonoBehaviour {

    public GameObject menuItems;
    public GameObject observationButtons;
    public InputField iField;
    public Text observationLabel;

    public bool menuEnabled = true;
    private string observationName = "";

    FileInfo f;
    private string observationData = "";
    private string startTime = "";
    private string startTimeNormal = "";

    private System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);


    private int passerby = 0,
    noticed = 0,
    smartphone = 0,
    smartphoneNoticed = 0,
    interacted = 0,
    smartphoneInteracted = 0;


    // Update is called once per frame
    void Update () {
        // Turn of main menu
        if (menuEnabled == false)
        {
            menuItems.SetActive(false);
            observationButtons.SetActive(true);
        }
	}

    public void observationCount(string countType)
    {
        switch (countType)
        {
            case "passer-by":
                passerby++;
                break;
            case "noticed":
                noticed++;
                break;
            case "smartphone":
                smartphone++;
                break;
            case "smartphonenoticed":
                smartphoneNoticed++;
                break;
            case "interacted":
                interacted++;
                break;
            case "smartphoneinteracted":
                smartphoneInteracted++;
                break;
            default:
                break;
        }
    }

    public void startObservation()
    {
        int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        startTime = cur_time.ToString();
        startTimeNormal = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        observationName = iField.text;
        observationLabel.text = observationName;
        Debug.Log("Observation name set to: " + observationName);
        menuEnabled = false;
    }


    public void endObservation()
    {
        int cur_time = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        f = new FileInfo(Application.persistentDataPath + "\\" + observationName + "_" + cur_time.ToString() + ".txt");
        Save();
    }

    void Save()
    {
        StreamWriter w;
        if (!f.Exists)
        {
            w = f.CreateText();
        }
        else
        {
            f.Delete();
            w = f.CreateText();
        }

        observationData = "\nPassers-by: " + passerby.ToString() + "\nNoticed: " + noticed.ToString() + "\nInteracted: " + interacted.ToString() + "\nSmartphone Users: " + smartphone.ToString() + "\nSmartphone & Noticed: " + smartphoneNoticed.ToString() + "\nSmartphone & Interacted: " + smartphoneInteracted.ToString() + "\n";

        w.WriteLine(observationName + "\n" + startTimeNormal + "\n\n" + observationData + "\n\n" + System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        w.Close();

        Application.Quit();
    }
}