using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    GameHelper gameHelper;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        gameHelper = GameObject.FindObjectOfType<GameHelper>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameHelper.CurrenpPlayer == null)
        {
            return;
        }

        playerTransform = gameHelper.CurrenpPlayer.transform;

        Vector3 newPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * 15);

        
    }
}
