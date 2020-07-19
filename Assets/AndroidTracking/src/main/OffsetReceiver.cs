using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uOSC
{

    [RequireComponent(typeof(uOscServer))]
    public class OffsetReceiver : MonoBehaviour
    {
        private const int DateNum = 3;
        void Start()
        {
            var server = GetComponent<uOscServer>();
            Debug.Log("ServerStartted");
            server.onDataReceived.AddListener(OnDataReceived);
        }

        void OnDataReceived(Message message)
        {
            // address
            var msg = "";// +  message.address + ": ";

            // timestamp
            // msg += "(" + message.timestamp.ToLocalTime() + ") ";
            float[] pos = new float[3];
            // values
            foreach (var value in message.values)
            {
                msg += value.GetString() + " ";
            }
            for (int i = 0; i < DateNum; i++)
            {
                pos[i] = (float)message.values[i];
            }

            // Debug.Log(msg);
            Debug.Log(message.values[2]);
            Debug.Log(pos[0] + "," + pos[1] + "," + pos[2]);
            SendPos.x = pos[0];
            SendPos.y = pos[1];
            SendPos.z = pos[2];
        }
    }

}
