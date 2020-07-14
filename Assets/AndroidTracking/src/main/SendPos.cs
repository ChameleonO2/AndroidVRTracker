using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class SendPos : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textDebug;

    public GameObject myPos;
    public InputField WaistPosInput;
    private int tenable = 1;
    private float timeoffset = 0F;
    private string ip;
    private int tid;

    //Pos offsetValue
    public float x = 0F, y = 0F, z = 0F;
    public float qx = 0F, qy = 0F, qz = 0F, qw = 0F;

    //Waist height
    private float waist = 0.85F;
    //オフセットの手動調整用の変化量
    private float adjustScale = 0.01F;

    private uOSC.uOscClient client;
    void Start()
    { 
        //VMT's Tracker ID
        this.tid = ChangeSCENE.tid;

        this.waist = ChangeSCENE.waist;

        //Init WaistPositionText 
        WaistPosInput.text = this.waist.ToString();

        Vector3 pos = myPos.transform.position;
        pos.y = waist;
        myPos.transform.position = pos;

        //Do not Sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //OSC Send Setting
        client = GetComponent<uOSC.uOscClient>();
        client.setIp(this.ip);
    }

    // Update is called once per frame
    void Update()
    {
        textDebug.text = "ip:" + this.ip + "\n";
        textDebug.text += "X:" + Frame.Pose.position.x + " \n"
                + "Y:" + Frame.Pose.position.y+"\n"
                +"Z:"+ Frame.Pose.position.z+"\n"
                +"qX:"+Frame.Pose.rotation.x+"\n"
                +"qY:"+Frame.Pose.rotation.y+"\n"
                +"qZ:"+Frame.Pose.rotation.z+"\n"
                +"qW:"+Frame.Pose.rotation.w+"\n";

        Vector3 pos = myPos.transform.position;
        pos.x = Frame.Pose.position.x - this.x;
        pos.y = Frame.Pose.position.y - this.y;
        pos.z = Frame.Pose.position.z - this.z;
        myPos.transform.position = pos;

        Quaternion qtmp = new Quaternion(Frame.Pose.rotation.x-qx, 
                                         Frame.Pose.rotation.y-qy,
                                         Frame.Pose.rotation.z-qz,
                                         Frame.Pose.rotation.w-qw);
        myPos.transform.rotation= qtmp;

        textDebug.text += "X:" + myPos.transform.position.x + " \n"
                + "Y:" + (myPos.transform.position.y+waist) + "\n"
                + "Z:" + myPos.transform.position.z  + "\n"
                + "qX:" + myPos.transform.rotation.x  + "\n"
                + "qY:" + myPos.transform.rotation.y  + "\n"
                + "qZ:" + myPos.transform.rotation.z  + "\n"
                + "qW:" + myPos.transform.rotation.w  + "\n"
                + "off_x:" + this.x+"\n"
                + "off_y:" + this.y+"\n"
                + "off_z:" + this.z+"\n";


        // Debug.Log("ip:"+this.ip+"id:"+tid.ToString()+"enable:"+tenable.ToString());
        client.Send("/VMT/Room/Unity",tid, tenable, timeoffset,
            (float)myPos.transform.position.x,
            (float)myPos.transform.position.y + waist,
            (float)myPos.transform.position.z,
            (float)myPos.transform.rotation.x,
            (float)myPos.transform.rotation.y,
            (float)myPos.transform.rotation.z,
            (float)myPos.transform.rotation.w
        );


    }
    public void onClick(int b_no){
        switch(b_no){
            case 0:
                ResetClick();
                break;
            case 1:
                this.x += adjustScale;
                break;
            case 2:
                this.x -= adjustScale;
                break;
            case 3:
                this.y += adjustScale;
                break;
            case 4:
                this.y -= adjustScale;
                break;
            case 5:
                this.z += adjustScale;
                break;
            case 6:
                this.z -= adjustScale;
                break;
            default:
                break;

        }
    }
    private void ResetClick(){
        WaistPosInput=WaistPosInput.GetComponent<InputField>();

        this.x= Frame.Pose.position.x;
        this.y= Frame.Pose.position.y;
        this.z= Frame.Pose.position.z;

        this.qx= Frame.Pose.rotation.x;
        this.qy= Frame.Pose.rotation.y;
        this.qz= Frame.Pose.rotation.z;
        this.qw= Frame.Pose.rotation.w-1F;

        this.waist = float.Parse(WaistPosInput.text) / 100;

        PlayerPrefs.SetFloat(configdate.WAIST,waist);
        PlayerPrefs.Save();

        Quaternion qtmp = new Quaternion(Frame.Pose.rotation.x-qx, 
                                         Frame.Pose.rotation.y-qy,
                                         Frame.Pose.rotation.z-qz,
                                         Frame.Pose.rotation.w-qw);
        myPos.transform.rotation= qtmp;
        textDebug.text = "ip:" + this.ip + "\n";

        textDebug.text += "X:" + myPos.transform.position.x + " \n"
                + "Y:" + (myPos.transform.position.y+waist) + "\n"
                + "Z:" + myPos.transform.position.z + "\n"
                + "qX:" + myPos.transform.rotation.x + "\n"
                + "qY:" + myPos.transform.rotation.y + "\n"
                + "qZ:" + myPos.transform.rotation.z + "\n"
                + "qW:" + myPos.transform.rotation.w + "\n"
                + "off_x:" + this.x+"\n"
                + "off_y:" + this.y+"\n"
                + "off_z:" + this.z+"\n"
                + "off_qx:" + this.qx + "\n"
                + "off_qy:" + this.qy + "\n"
                + "off_qz:" + this.qz + "\n"
                + "off_q2:" + this.qw + "\n";
    }
}
