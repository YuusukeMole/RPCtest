using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class NetworkConnector : MonobitEngine.MonoBehaviour
{
    private int i=0;
    private int sender_i = 0;
    public GameObject prefab;
    private GameObject recv=null;

    [MunRPC]
    void RecvRPC(int senderNumber)
    {
        sender_i = senderNumber;
    }

    private void OnGUI()
    {
        if(MonobitNetwork.isConnect)
        {
            if (MonobitNetwork.inRoom)
            {
                if(recv == null)
                {
                    recv = MonobitNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity, 0);
                }

                if (GUILayout.Button("Push", GUILayout.Width(100)))
                {
                    i++;
                    monobitView.RPC("RecvRPC", MonobitTargets.All, i);
                }

                GUILayout.Label("sender_i = " + sender_i);
            }

            else
            {
                if (GUILayout.Button("Create Room", GUILayout.Width(150)))
                {
                    MonobitNetwork.CreateRoom("test");
                }

                foreach (RoomData room in MonobitNetwork.GetRoomData())
                {
                    if (GUILayout.Button("EnterRoom:" + room.name))
                    {
                        MonobitNetwork.JoinRoom(room.name);
                    }
                }
            }
        }
        else
        {
            if(GUILayout.Button("Connect Server",GUILayout.Width(150)))
            {
                MonobitNetwork.ConnectServer("RPCtest");
                MonobitNetwork.autoJoinLobby = true;
            }
        }
    }
}
