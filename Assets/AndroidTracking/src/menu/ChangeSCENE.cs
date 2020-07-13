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
    public InputField waistInput;

    public static string ip;
    public static int tid;
    public static float waist;

    // Start is called before the first frame update
    void Start(){

        if(PlayerPrefs.HasKey(configdate.pcaddress)){
            ipAdress.text = PlayerPrefs.GetString(configdate.pcaddress);
        }

        if(PlayerPrefs.HasKey(configdate.VMTID)){
            trackerId.text = PlayerPrefs.GetInt(configdate.VMTID).ToString();
        }else{
            trackerId.text = "0"; //デフォルト値
        }

        if(PlayerPrefs.HasKey(configdate.WAIST)){
            waistInput.text = PlayerPrefs.GetFloat(configdate.WAIST).ToString();
        }else{
            waistInput.text = "85"; //デフォルト値
        }

    }

    public void OnClick(){

        ipAdress = ipAdress.GetComponent<InputField>();
        ip = ipAdress.text;
        trackerId = trackerId.GetComponent<InputField>();
        tid = int.Parse(trackerId.text);

        waistInput = waistInput.GetComponent<InputField>();
        waist= float.Parse(waistInput.text);

        PlayerPrefs.SetString(configdate.pcaddress,ip);
        PlayerPrefs.SetInt(configdate.VMTID,tid);
        PlayerPrefs.SetFloat(configdate.WAIST,waist);
        PlayerPrefs.Save();

        //don't work:(
        StartCoroutine(ExampleCoroutine());

        SceneManager.LoadScene("TrackingScene");
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(5);
    }
}

