using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class Reciver : MonobitEngine.MonoBehaviour
{
    private int sender_i = 0;
    private int j = 0;
    bool recvFlag = false;

    [MunRPC]
    void RecvRPC(int senderNumber)
    {
        sender_i = senderNumber;
        recvFlag = true;
    }

    private void Update()
    {
        if(recvFlag)
        {
            Debug.Log("sender_i = " + sender_i);
            recvFlag = false;
        }
    }

    private void OnGUI()
    { 
        GUILayout.Space(48);
        if (GUILayout.Button(" Object Push", GUILayout.Width(120)))
        {
            j++;
            monobitView.RPC("RecvRPC", MonobitTargets.All, j);
        }
        if(!monobitView.isMine)
        {
            GUILayout.Space(24);
        }
        GUILayout.Label("sender_ j = " + sender_i);
    }
}
