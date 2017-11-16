using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class Reciver2 : MonobitEngine.MonoBehaviour
{

    private int sender_i = 0;
    bool recvFlag = false;

    [MunRPC]
    void RecvRPC(int senderNumber)
    {
        sender_i = senderNumber;
        recvFlag = true;
    }

    private void Update()
    {
        if (recvFlag)
        {
            Debug.Log("Other Script sender_i = " + sender_i);
            recvFlag = false;
        }
    }
}
