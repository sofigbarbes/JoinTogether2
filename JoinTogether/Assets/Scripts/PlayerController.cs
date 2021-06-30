using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{

    private float MovementSpeed = 3;
    private float JumpForce = 4.5f;
    private int totalPoints = 0;

    private float vertical;
    private bool isLadder;
    private bool isClimbing;

    public static event Action Died;
    public static event Action Win;

    private Rigidbody2D rigidbody;
    [SerializeField]
    private Camera mainCamera;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        updatePosition();
        
        if(Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody.velocity.y) < 0.01){
            rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        vertical = Input.GetAxis("Vertical");

        if(isLadder && Mathf.Abs(vertical)>0f){
            isClimbing = true;
        }
    }

    private void updatePosition(){
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        mainCamera.transform.position = new Vector3(transform.position.x,
                         transform.position.y,  mainCamera.transform.position.z);
    }
    private void FixedUpdate(){
        if(isClimbing){
            rigidbody.gravityScale = 0f;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, vertical * MovementSpeed);
        }
        else{
            rigidbody.gravityScale = 1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        
        string tag = collision.tag;
        switch (tag){
            case "Ladder":
                isLadder = true;
                break;
            case "Loose":
                Debug.Log("Pinchos");
                Died.Invoke();
                break;
            case "Points":
                totalPoints +=1;

                Destroy(collision.gameObject);
                Debug.Log("Points: "+totalPoints);
                /*rigidbody.transform.localScale = new Vector3(rigidbody.transform.localScale.x*1.1f,
                                    rigidbody.transform.localScale.y*1.1f);*/
                rigidbody.GetComponent<BoxCollider2D>().transform.localScale = new Vector3(rigidbody.transform.localScale.x*1.1f,
                                    rigidbody.transform.localScale.y*1.1f);
                break;
            case "FinishLine":
                if(totalPoints>=16)
                {
                    Destroy(collision.gameObject);
                    Debug.Log("Finish");
                    Win.Invoke();
                }
                Debug.Log(totalPoints);

                break;
            case "Breakable8":
                if(totalPoints>=8)
                {
                    Destroy(collision.gameObject);
                    Debug.Log("Next");
                }
                Debug.Log(totalPoints);

                break;
            case "Cofre":
               Win.Invoke();
            break;
                
        }
        

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
        }
    }
}
