using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Linq;
using System;
using System.Collections.Generic;



public class NetworkCore : MonoBehaviour {
    public GameObject player;
    //   public static List<NetworkGameObject> ObjectReference = new List<NetworkGameObject>();
    public static List<NetworkGameObject2D> ObjectReference2D = new List<NetworkGameObject2D>();

    public GameObject obj;
    static Socket client;
    public static void Connect(string ip, int port) {
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        client.Connect(ip, port);
        getAllObject();
        GameObject prefab = (GameObject)Resources.Load("test/Player1");
        Instantiates(prefab);


        GameObject car = (GameObject)Resources.Load("Cart/Prefab/Cart1");
        Instantiates(car);

    }


    void received() {
        bool receive_Size = true;
        byte[] buffer = new byte[1];
        int bytesRec = 0;

        if (receive_Size && client.Available >= 1) {
            receive_Size = false;
            buffer = new byte[1];
            client.Receive(buffer, buffer.Length, SocketFlags.None);
            bytesRec = buffer[0];
        }

        if (!receive_Size && client.Available >= bytesRec) {
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

    private static void getAllObject() {

        byte[] length = new byte[] { (byte)1 };
        byte[] code = new byte[] { (byte)3 };
        length = length.AddByte(code);
        client.Send(length);

    }
    private static void Send(NetworkGameObject2D obj, int Code) {
        byte[] data = obj.ToByte();
        byte[] length = new byte[] { (byte)(data.Length + 1) };
        byte[] code = new byte[] { (byte)Code };
        length = length.AddByte(code);
        length = length.AddByte(data);
        client.Send(length);

    }
    private static void Send(string obj, int Code) {
        byte[] data = obj.ToByte();
        byte[] length = new byte[] { (byte)(data.Length + 1) };
        byte[] code = new byte[] { (byte)Code };
        length = length.AddByte(code);
        length = length.AddByte(data);
        client.Send(length);

    }

    public static void Instantiates(GameObject Gameobj) {

        if (!client.Connected)
            return;


        NetworkGameObject2D obj = new NetworkGameObject2D();
        obj.gameObject = Instantiate(Gameobj);
        obj.gameObject.name = Gameobj.name;
        obj.IsMine = true;

        //obj.moveLerp = obj.gameObject.GetComponent<MoveLerp2D>();
        // obj.moveLerp.IsMine = true;
        ObjectReference2D.Add(obj);
        Camera.main.GetComponent<CmaeraRunnerScript>().Player = obj.gameObject.transform;

        byte[] data = obj.ToByte(obj.gameObject.GetInstanceID());
        byte[] length = new byte[] { (byte)(data.Length + 1) };
        byte[] code = new byte[] { 1 };
        length = length.AddByte(code);
        length = length.AddByte(data);
        client.Send(length);

    }


    void setID(byte[] byteArray) {
        int currentID = byteArray.Take(4).ToArray().ToInt();
        int NewId = byteArray.Skip(4).Take(4).ToArray().ToInt();
        ObjectReference2D.First(x => x.gameObject.GetInstanceID() == currentID).ID = NewId;
    }

    void newObject(byte[] buffer) {
        NetworkGameObject2D result = buffer.NewGameObject2D();
        result.gameObject = (GameObject)Instantiate(result.gameObject, result.position, result.rotation);
        MoveLerp2D moveLerp2D = new MoveLerp2D();
        result.moveLerp = result.gameObject.gameObject.AddComponent<MoveLerp2D>();
        //  result.moveLerp = result.gameObject.GetComponent<MoveLerp2D>();
        ObjectReference2D.Add(result);
        result.gameObject.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().enabled = false;
    }

    void work(int Code, byte[] buffer) {
        if (Code == 2) {
            setID(buffer);
        }
        if (Code == 3) {
            newObject(buffer);
        }
        if (Code == 4) {
            update(buffer);
        }
        if (Code == 5) {
            NetworkDestroyObject(buffer);
        }
        if (Code == 255) {
            Send("", 255);
        }

    }

    void update(byte[] byteArray) {
        NetworkGameObject2D ReceiveObject = byteArray.GetGameObject2D();
        var target = ObjectReference2D.First(x => x.ID == ReceiveObject.ID);
        target.position = ReceiveObject.position;
        target.rotation = ReceiveObject.rotation;
        target.moveLerp.position = ReceiveObject.position;
        target.moveLerp.rotation = ReceiveObject.rotation;
        target.moveLerp.offset = 0;

    }

    void Start() {
        //Instantiates(player);
    }

    float time;
    public void SyncObject() {
        if ((Time.time - time) > 0.1f) {
            time = Time.time;
            foreach (var item in ObjectReference2D.Where(x => x.ID != 0 && x.IsMine && ((Vector2)x.gameObject.transform.position != x.position || x.gameObject.transform.rotation != x.rotation || (Time.time - time) > 2))) {
                item.position = item.gameObject.transform.position;
                item.rotation = item.gameObject.transform.rotation;
                Send(item, 2);//update
            }
        }
    }

    void Update() {
        if (client != null && client.Connected) {
            received();
            SyncObject();
        }
    }

    void NetworkDestroyObject(byte[] byteArray) {
        var destroyObject = ObjectReference2D.First(x => x.ID == byteArray.ToInt());
        ObjectReference2D.Remove(destroyObject);
        Destroy(destroyObject.gameObject);

    }
}
