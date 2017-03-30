using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimmyAstar : MonoBehaviour {

    /*public class Noeud
    {
        public Vector3 position;
        public List<Vector3> noeudVoisin;
        
        public Noeud(Vector3 _position, List<Vector3> _noeudVoisin)
        {
            this.position = _position;
            this.noeudVoisin = _noeudVoisin;
        }

        public bool isNoeudVoisin(Vector3 _position)
        {
            bool result = false;
            Vector3[] listNoeud = noeudVoisin.ToArray();
            int index = 0;
            do
            {
                if (listNoeud[index] == _position)
                {
                    result = true;
                }
                index++;
            } while (result == false && index < listNoeud.Length);
            return result;
        }

        public float distance(Vector3 _position)
        {
            return Vector3.Distance(this.position, _position);
        }

    } */


    public GameObject timmy;
    public GameObject cheminTimmy;
   // public List<Noeud> listNoeud;

    // Use this for initialization
    void Start ()
    {
        // listNoeud = new List<Noeud>();
        //-- le chemin A* de Timmy
        //-- noeud 0
        /* Vector3[] input = {cheminTimmy.transform.GetChild(1).transform.position,
             cheminTimmy.transform.GetChild(2).transform.position,
             cheminTimmy.transform.GetChild(3).transform.position};

         listNoeud.Add(new Noeud (cheminTimmy.transform.GetChild(0).transform.position , new List<Vector3>(input))); */

        //-- test
        Debug.Log(" le noeud 1 est il voisin du noeud 0? réponse: " + cheminTimmy.transform.GetChild(0).GetComponent<Noeud>().isNoeudVoisin(cheminTimmy.transform.GetChild(3).transform.position));
    }

    // Update is called once per frame
    void Update () {
		
	}
}
