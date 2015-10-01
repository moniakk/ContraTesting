using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Linq;
using System;
using System.Collections.Generic;



public class NetworkCore : MonoBehaviour
{
    public GameObject player;
    public static List<NetworkGameObject> ObjectReference = new List<NetworkGameObject>();
    public GameObject obj;
    static Socket client;
    public static void Connect(string ip, int port)
    {
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client.Connect(ip, port);
        getAllObject();
        GameObject prefab = (GameObject)Resources.Load("test/Player1");
        Instantiates(prefab);

    }


    void received()
    {
        bool receive_Size = true;
        byte[] buffer = new byte[1];
        int bytesRec = 0;

        if (receive_Size && client.Available >= 1)
        {
            receive_Size = false;
            buffer = new byte[1];
            client.Receive(buffer, buffer.Length, SocketFlags.None);
            bytesRec = buffer[0];
        }

        if (!receive_Size && client.Available >= bytesRec)
        {
            receive_Size = true;
            buffer = new byte[bytesRec];
            client.Receive(buffer, buffer.Length, SocketFlags.None);

            byte[] tmp = new byte[buffer.Length];
            Array.Copy(buffer, tmp, tmp.Length);
            var Code = tmp[0];
            // GameCore.GameCore.receive(client, id, tmp.Skip(4).ToArray(), Code);
            work(Code, tmp.Skip(1).ToArray());
        }
    }

    private static void getAllObject()
    {

        byte[] length = new byte[] { (byte)1 };
        byte[] code = new byte[] { (byte)3 };
        length = length.AddByte(code);
        client.Send(length);

    }
    private static void Send(NetworkGameObject obj, int Code)
    {
        byte[] data = obj.ToByte();
        byte[] length = new byte[] { (byte)(data.Length + 1) };
        byte[] code = new byte[] { (byte)Code };
        length = length.AddByte(code);
        length = length.AddByte(data);
        client.Send(length);

    }

    public static void Instantiates(GameObject Gameobj)
    {

        if (!client.Connected)
            return;


        NetworkGameObject obj = new NetworkGameObject();
        obj.gameObject = Instantiate(Gameobj);
        obj.gameObject.name = "test/Player1";
        obj.IsMine = true;

        obj.moveLerp = obj.gameObject.GetComponent<MoveLerp>();
        obj.moveLerp.IsMine = true;
        ObjectReference.Add(obj);
        Camera.main.GetComponent<CmaeraRunnerScript>().Player = obj.gameObject.transform;

        byte[] data = obj.ToByte(obj.gameObject.GetInstanceID());
        byte[] length = new byte[] { (byte)(data.Length + 1) };
        byte[] code = new byte[] { 1 };
        length = length.AddByte(code);
        length = length.AddByte(data);
        client.Send(length);

    }


    void setID(byte[] byteArray)
    {
        int currentID = byteArray.Take(4).ToArray().ToInt();
        int NewId = byteArray.Skip(4).Take(4).ToArray().ToInt();
        ObjectReference.First(x => x.gameObject.GetInstanceID() == currentID).ID = NewId;

    }

    void newObject(byte[] buffer)
    {
        NetworkGameObject result = buffer.NewGameObject();
        result.gameObject = (GameObject)Instantiate(result.gameObject, result.position, result.rotation);
        result.moveLerp = result.gameObject.GetComponent<MoveLerp>();
        ObjectReference.Add(result);
        result.gameObject.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = false;
       


    }

    void work(int Code, byte[] buffer)
    {
        if (Code == 2)
        {
            setID(buffer);
        }
        if (Code == 3)
        {
            newObject(buffer);
        }
        if (Code == 4)
        {
            update(buffer);
        }

    }

    void update(byte[] byteArray)
    {
        NetworkGameObject ReceiveObject = byteArray.GetGameObject();
        var target = ObjectReference.First(x => x.ID == ReceiveObject.ID);
        target.position = ReceiveObject.position;
        target.rotation = ReceiveObject.rotation;
        target.moveLerp.position = ReceiveObject.position;
        target.moveLerp.rotation = ReceiveObject.rotation;
        target.moveLerp.offset = 0;

    }

    void Start()
    {
        //Instantiates(player);
    }

    float time;
    public void SyncObject()
    {
        if ((Time.time - time) > 0.1f)
        {
            time = Time.time;
            foreach (var item in ObjectReference.Where(x => x.ID != 0 && x.IsMine && (x.gameObject.transform.position != x.position || x.gameObject.transform.rotation != x.rotation)))
            {
                item.position = item.gameObject.transform.position;
                item.rotation = item.gameObject.transform.rotation;
                Send(item, 2);//update
            }
        }
    }

    void Update()
    {
        if (client != null && client.Connected)
        {
            received();
            SyncObject();

        }

    }


}
