using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class ChatScript : MonobitEngine.MonoBehaviour
{
    private string roomName = "";
    private string chatWord = "";
    List<string> chatLog = new List<string>();

    [MunRPC]
    void RecvChat(string senderName,string senderWord)
    {
        chatLog.Add(senderName + ":" + senderWord);
        if(chatLog.Count > 10)
        {
            chatLog.RemoveAt(0);
        }
    }

    private void OnGUI()
    {
        if(MonobitNetwork.isConnect)
        {
            if(MonobitNetwork.inRoom)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("PlayerList:");
                foreach (MonobitPlayer player in MonobitNetwork.playerList)
                {
                    GUILayout.Label(player.name+"");
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Message:");
                chatWord = GUILayout.TextField(chatWord, GUILayout.Width(400));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("send", GUILayout.Width(100)))
                {
                    monobitView.RPC("RecvChat", MonobitTargets.All, MonobitNetwork.playerName, chatWord);
                }

                string msg = "";
                for(int i = 0;i<10; ++i)
                {
                    msg += ((i < chatLog.Count) ? chatLog[i] : "") + "\r\n";
                }
                GUILayout.TextArea(msg);

                if(GUILayout.Button("Leave Room",GUILayout.Width(150)))
                {
                    MonobitNetwork.LeaveRoom();
                    chatLog.Clear();
                }
            }
            else
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Room Name:");
                roomName = GUILayout.TextField(roomName, GUILayout.Width(200));
                GUILayout.EndHorizontal();

                if(GUILayout.Button("Create Room",GUILayout.Width(150)))
                {
                    MonobitNetwork.CreateRoom(roomName);
                }

                foreach(RoomData room in MonobitNetwork.GetRoomData())
                {
                    if(GUILayout.Button("EnterRoom:"+room.name))
                    {
                        MonobitNetwork.JoinRoom(room.name);
                    }
                }
            }

        }
        else
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Player Name:");
            MonobitNetwork.playerName = GUILayout.TextField((MonobitNetwork.playerName == null) ? "" : MonobitNetwork.playerName, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            MonobitNetwork.autoJoinLobby = true;

            if(GUILayout.Button("Connect Server",GUILayout.Width(150)))
            {
                MonobitNetwork.ConnectServer("ChatTest");
            }
        }
    }
}
