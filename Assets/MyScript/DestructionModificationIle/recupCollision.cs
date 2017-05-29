using UnityEngine;
using System.Collections;

public class recupCollision : MonoBehaviour {

    private string nom=null;

    void onCollisionEnter(Collision collision)
    {
        nom = collision.gameObject.name;
    }

    public string getNomCollision()
    {
        return nom;
    }
}
