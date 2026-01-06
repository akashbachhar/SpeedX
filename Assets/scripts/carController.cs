using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    public float carSpeed;
    Vector3 position;
    public uiManager ui;
    bool currentPlatformAndroid = false;

    Rigidbody2D rb;

    public audioManager am;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();

        #if UNITY_ANDROID
            currentPlatformAndroid = true;
        #else
            currentPlatformAndroid = false;
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    
    void Update()
    {

        if (currentPlatformAndroid == true){
            AccelerometerMove();
        }
        else{
            position.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
            position.x = Mathf.Clamp(position.x, -2.2f, 2.2f);
            transform.position = position;
        }

        position = transform.position;
        position.x = Mathf.Clamp(position.x, -2.2f, 2.2f);
        transform.position = position;
      
    }

    void AccelerometerMove(){
       float x  = Input.acceleration.x;
       
       if (x < -0.1f){
           rb.velocity = new Vector2(x * carSpeed, 0) * 2f;
       }
       else if (x > 0.1f){
           rb.velocity = new Vector2(x * carSpeed, 0) *2f;
       }
       else{
           rb.velocity = Vector2.zero;
       }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy Car")
        {
            Destroy(gameObject);
            ui.GameOver();
            am.explosion.Play();
        }
    }
}