using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class model_controller : MonoBehaviour
{
    public float speed = 25;
    public float rotationSpeed=10;
    float rot = 0f;
    float rotspeed = 80;
    float gravity = 8;
    Rigidbody rb;
    Vector3 moveDirection;
    public GameObject bulletPrefab;
    public Transform BulletPos;

    Vector3 movDir = Vector3.zero;
    Animator anim;

    public float Health;
    int damage =30;
    int bossdamage = 60;

    public Slider slider;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        slider.value = 500;
        Health = slider.value;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("Mouse X") < 0)
            transform.Rotate(Vector3.up * -rotationSpeed);
        if (Input.GetAxis("Mouse X") > 0)
            transform.Rotate(Vector3.up * rotationSpeed);

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
         moveDirection = transform.forward * Vertical * speed + transform.right * Horizontal * speed;


        anim.SetFloat("movement", Mathf.Max(Mathf.Abs(Vertical), Mathf.Abs(Horizontal)));
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && Mathf.Approximately(Mathf.Max(Vertical, Horizontal), 0) )
        {
            anim.SetTrigger("attack");
            Shoot();
        }

       
    }
    

    void FixedUpdate()
    {       
        Move(moveDirection);
    }
    void Move(Vector3 dir)
    {
        
        rb.MovePosition(transform.position + dir * Time.fixedDeltaTime);
        

    }
    
    void Shoot()
    {
       GameObject b = Instantiate(bulletPrefab, BulletPos.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().AddForce(transform.forward * 20, ForceMode.Impulse);
    }
    public void ReduceHealth(float damage)
    {  

        slider.value -= damage;
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }

    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag=="Enemy")
        {
            slider.value -= damage;
            Health -= damage;
            if (Health <= 0)
            {
                SceneManager.LoadScene(3);

            }
        }
        if (collision.gameObject.tag=="ENDBOSS")
        {
            slider.value -= bossdamage;
            Health -= bossdamage;
            if (Health <= 0)
            {
                SceneManager.LoadScene(3);

            }
        }
    }

}
