using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    public GameObject Parent;

    public Renderer rend;
    public Material Base;
    public Material Dead;
    bool ChangeChild;

    void Update()
    {
        ParentState();
    }

    void ParentState(){
        if(Parent.GetComponent<playerControls>().isDead == true){
            rend.material = Dead;
        } else{
            rend.material = Base;
        }
    }
}
