using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSCENE : MonoBehaviour
{
    private string ipname = "PCADRESS";
    public InputField ipAdress;
    public InputField trackerId;
    public static string ip;
    public static int tid = 1;

    // Start is called before the first frame update
    void Start(){
        trackerId.text = "1";
        if(PlayerPrefs.HasKey(configdate.pcaddress)){
            ipAdress.text = PlayerPrefs.GetString(configdate.pcaddress);
        }
    }
    public void OnClick(){
        ipAdress = ipAdress.GetComponent<InputField>();
        ip = ipAdress.text;
        trackerId = trackerId.GetComponent<InputField>();
        tid = int.Parse(trackerId.text);
        Debug.Log("loadScene");
        PlayerPrefs.SetString(configdate.pcaddress,ip);
        PlayerPrefs.Save();
        StartCoroutine(ExampleCoroutine());
        SceneManager.LoadScene("TrackingScene");
    }
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);
    }
}

