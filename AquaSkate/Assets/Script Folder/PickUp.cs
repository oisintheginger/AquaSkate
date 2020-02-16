using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool isGood, slows = false, speeds = false, slowSteering = false, speedsSteering = false;

    [SerializeField] float storedSteering, storedMaxSpeed;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            var pM = other.gameObject.GetComponent<playerMotion>();
            var rB = other.gameObject.GetComponent<Rigidbody>();


        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
