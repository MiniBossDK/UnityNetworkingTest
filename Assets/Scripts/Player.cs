using Mirror;
using UnityEngine;
public class Player : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnHelloCountChanged))]
    int helloCount = 0;
    
    void HandleMovement() 
    {
        if(isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.3f,  moveVertical * 0.3f, 0);
            transform.position = transform.position + movement;
        }
    }

    void Update() 
    {
        HandleMovement();

        if(isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Sending Hello to Server");
            Hello();
        }
    }

    [Command]
    void Hello()
    {
        Debug.Log("Received Hello from Client");
        helloCount += 1;
        ReplyHello();
    }

    [TargetRpc]
    void ReplyHello() 
    {
        Debug.Log("Received Hello from Server!");
    }

    [ClientRpc]
    void TooHigh() 
    {
        Debug.Log("Too High!");
    }

    void OnHelloCountChanged(int oldCount, int newCount) 
    {
        Debug.Log($"We had {oldCount} hellos, but now we have {newCount} hellos");
    }
}
