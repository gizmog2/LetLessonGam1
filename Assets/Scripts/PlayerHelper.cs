using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Accessibility;
//using UnityEngine.Networking;

public class PlayerHelper : NetworkBehaviour
{   
    public NetworkVariable<float> Speed = new NetworkVariable<float>(10);
    public NetworkVariable<float> Size = new NetworkVariable<float>(0.1f);
    [SerializeField] NetworkVariable<Color> _Color = new NetworkVariable<Color>(Color.blue);

    GameHelper gameHelper;

    /*public float Speed = 10;

    public float Size = 0.1f;*/

    // Start is called before the first frame update
    void Start()
    {       
        gameHelper = GameObject.FindObjectOfType<GameHelper>();

        if (!IsLocalPlayer)
        {
            return;
        }
        gameHelper.CurrenpPlayer = this;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(Size.Value, Size.Value, Size.Value);

        GetComponent<SpriteRenderer>().color = _Color.Value;

        if (!IsLocalPlayer)
        {
            return;
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = Vector3.MoveTowards(transform.position, mousePos, Time.deltaTime * Speed.Value);
        CheckBounds();
        
    }

    void CheckBounds()
    {
        if (transform.position.x >= gameHelper.MapSize.x)
        {
            transform.position = new Vector2(gameHelper.MapSize.x - 0.01f, transform.position.y);
        }
        if (transform.position.y >= gameHelper.MapSize.y)
        {
            transform.position = new Vector2(transform.position.x, gameHelper.MapSize.y - 0.01f);
        }


        if (transform.position.x <= -gameHelper.MapSize.x)
        {
            transform.position = new Vector2(-gameHelper.MapSize.x + 0.01f, transform.position.y);
        }
        if (transform.position.y <= -gameHelper.MapSize.y)
        {
            transform.position = new Vector2(transform.position.x, -gameHelper.MapSize.y + 0.01f);
        }
    }

    //[ServerRpc]
    public void ChangeSize(float size)
    {
        Size.Value += size;
        Speed.Value -= 10 * size;
        transform.localScale = new Vector3(Size.Value, Size.Value, Size.Value);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsServer)
        {
            return;
        }
        Bounds enemy = collision.bounds;
        Bounds current = GetComponent<CircleCollider2D>().bounds;

        Vector2 centerEnemy = enemy.center;
        Vector2 centerCurrent = current.center;

        //Debug.Log("centerEnemy = " + centerEnemy + "/centerCurrent" + centerCurrent + "/Dist = " + Vector2.Distance(centerEnemy, centerCurrent));

        if (current.size.x > enemy.size.x && Vector2.Distance(centerCurrent, centerEnemy) < current.size.x)
        {
            if (collision.GetComponent<PointHelper>())
            {
                ChangeSize(0.05f);
                gameHelper.CreatePointServerRpc(Color.yellow);
            }
            else
            {
                ChangeSize(collision.transform.localScale.x);
            }

            //ChangeSizeServerRpc(0.05f);
            NetworkObject.Destroy(collision.gameObject);
        }

    }
        
}
