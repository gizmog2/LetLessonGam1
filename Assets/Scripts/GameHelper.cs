using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
//using System.Drawing;
//using UnityEngine.Networking;

public class GameHelper : NetworkBehaviour
{
    public Vector2 MapSize = new Vector2(20, 20);

    private PlayerHelper _playerHelper;

    public PlayerHelper CurrenpPlayer
    {
        get { return _playerHelper; }
        set { _playerHelper = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.OnServerStarted += StartServerRpc;
    }

    [ServerRpc]
    void StartServerRpc()
    {
        /*if (IsServer)
        {
            Debug.Log("GameHelper Start ()");
            for (int i = 0; i < 10; i++)
            {
                CreatePoint(Color.yellow);
            }

        }*/
        Debug.Log("GameHelper Start ()");
        for (int i = 0; i < 10; i++)
        {
            CreatePointServerRpc(UnityEngine.Color.green);
        }

    }
    

    [ServerRpc]
    public void CreatePointServerRpc(Color color)
    {
        GameObject point = Instantiate<GameObject>(Resources.Load<GameObject>("Point"));
        Vector2 rand = Random.insideUnitCircle;        
        point.transform.position = rand * Random.Range(5, MapSize.x);
        point.GetComponent<NetworkObject>().Spawn();
        point.GetComponent<PointHelper>()._Color.Value = color;
        //NetworkObject.Spawn(point);
        //NetworkManager.Singleton.OnServerStarted -= StartServer; 
    }

    /*[ClientRpc]*//*
    public void PointColor(GameObject point, Color color)
    {
        point.GetComponent<SpriteRenderer>().color = color;
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
}
