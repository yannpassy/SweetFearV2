using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noeud : MonoBehaviour {
    
    public GameObject[] noeudVoisin;

    public float distance(Vector3 _position)
    {
        return Vector3.Distance(this.transform.position, _position);
    }

    public bool isNoeudVoisin(Vector3 _position)
    {
        bool result = false;
        int index = 0;
        do
        {
            if (noeudVoisin[index].transform.position == _position)
            {
                result = true;
            }
            index++;
        } while (result == false && index < noeudVoisin.Length);
        return result;
    }
}
