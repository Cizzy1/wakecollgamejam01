using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControls : MonoBehaviour
{
    //Post Processing GameObjects
    public GameObject normalVision;
    public GameObject ghostVision;

    //Attack 
    int Damage = 10;
    int AttackRange = 2;
    float NextAttack = .5f;
    float AttackRate = .5f;

    //General things
    float movespeed = 5;
    public float RotationSpeed = 500f;
    public Rigidbody rb;
    public Collider PlayerCol;
    public LayerMask TotemMask;
    public LayerMask PassiveWall;

    //Health related & changing of player state
    public int Health = 40;
    public Text HealthTxt;
    public Text CurrentStateTxt;
    public Material Mortal_Mat;
    public Material Ghost_Mat;
    public bool isDead;
    public bool WallCheckBool;

    //Renderer
    public Renderer rend;

    void Update()
    {

        HealthTxt.text = Health.ToString();

        CheckDeath();
        PlayerDamage();
        playerAttack();

        //Health caps
        if(Health < 0){
            Health = 0;
        }

        if(Health > 40){
            Health = 40;
        }

        //Change state
        if(isDead && WallCheckBool){
            PlayerCol.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }  else{
            PlayerCol.enabled = true;
        }  

        //Totem detection 
        RaycastHit hit;

        if(Physics.SphereCast(transform.position, 1, transform.forward, out hit, .5f, TotemMask) && isDead){
            Debug.Log(hit.ToString());
            Health += 40;
            Destroy(hit.transform.gameObject);
        }

        //Tag detection
        RaycastHit test;

        if(Physics.SphereCast(transform.position, .5f, transform.forward, out test, .5f)){
            if(test.transform.gameObject.tag == "walkable"){
                Debug.Log("Pass through wall");
                WallCheckBool = true;
            } else{
                WallCheckBool = false;
                Debug.Log("cant pass through that");
            }
        }

    }

    //Player rotation based on inputs
    void FixedUpdate(){
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rb.MovePosition(rb.position + move * movespeed * Time.fixedDeltaTime);

        if(move != Vector3.zero){
            Quaternion toRoatation = Quaternion.LookRotation(move, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRoatation, RotationSpeed * Time.fixedDeltaTime);
        }  
    }



    void CheckDeath(){
        if(Health > 1){
            isDead = false;
            rend.material = Mortal_Mat;
            CurrentStateTxt.text = "Current form: Mortal";
            //Sets Ghost vision to false and Normal vision to true
            ghostVision.SetActive(false);
            normalVision.SetActive(true);

        } else{
            isDead = true;
            CurrentStateTxt.text = "Current form: Ghost";
            rend.material = Ghost_Mat;
            //Sets Normal vision to false and Ghost vision to true
            ghostVision.SetActive(true);
            normalVision.SetActive(false);

        }
    }

    void PlayerDamage(){
        if(Input.GetKeyDown(KeyCode.E)){
            Health -= Damage;
        }
    }

    void playerAttack(){
        
    }

}
