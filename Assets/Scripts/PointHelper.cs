using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PointHelper : NetworkBehaviour
{
    public NetworkVariable<Color> _Color = new NetworkVariable<Color>(Color.blue);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().color = _Color.Value;
    }
}
