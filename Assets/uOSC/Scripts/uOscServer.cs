﻿using UnityEngine;
using UnityEngine.Events;

namespace uOSC
{

public class uOscServer : MonoBehaviour
{
    [SerializeField]
    int port = 3333;

#if NETFX_CORE
    Udp udp_ = new Uwp.Udp();
    Thread thread_ = new Uwp.Thread();
#else
    Udp udp_ = new DotNet.Udp();
    Thread thread_ = new DotNet.Thread();
#endif
    Parser parser_ = new Parser();

    public class DataReceiveEvent : UnityEvent<Message> {};
    public DataReceiveEvent onDataReceived { get; private set; }

    void Awake()
    {
        onDataReceived = new DataReceiveEvent();
    }

    void OnEnable()
    {
        udp_.StartServer(port);
            Debug.Log("OSC STERTED:" + port);

        thread_.Start(UpdateMessage);
    }

    void OnDisable()
    {
        Debug.Log("Disable OSC");
        thread_.Stop();
        udp_.Stop();
    }

    void Update()
    {
            Debug.Log("Update OSC");
            while (parser_.messageCount > 0)
        {
            var message = parser_.Dequeue();
                Debug.Log("UpdateOSC INFO:" + message.values[0]);
                onDataReceived.Invoke(message);
        }
    }

    void UpdateMessage()
    {
        while (udp_.messageCount > 0) 
        {
            var buf = udp_.Receive();
            int pos = 0;
            parser_.Parse(buf, ref pos, buf.Length);
        }
    }
}

}