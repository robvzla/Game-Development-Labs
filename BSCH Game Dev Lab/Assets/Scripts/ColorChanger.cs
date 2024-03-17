using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Material testMat;
    // Start is called before the first frame update
    void Start()
    {
        testMat = GetComponent<MeshRenderer>().material; // assigning material from this game object's mesh to the testMat variable
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        testMat.color = Random.ColorHSV(); // Changes the material's color to a random color
    }
}
