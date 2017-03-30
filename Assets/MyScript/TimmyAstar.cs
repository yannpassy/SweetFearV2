using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimmyAstar : MonoBehaviour {

    public class Noeud
    {
        public GameObject capsule;
        public List<GameObject> noeudVoisin;
        
        public Noeud(GameObject _capsule, List<GameObject> _noeudVoisin)
        {
            this.capsule = _capsule;
            this.noeudVoisin = _noeudVoisin;
        }

        public bool isNoeudVoisin(GameObject _capsule)
        {
            bool result = false;
            GameObject[] listNoeud = noeudVoisin.ToArray();
            int index = 0;
            do
            {
                if (listNoeud[index].transform.position == _capsule.transform.position)
                {
                    result = true;
                }
                index++;
            } while (result == false && index < listNoeud.Length);
            return result;
        }

        public float distance(GameObject _capsule)
        {
            return Vector3.Distance(this.capsule.transform.position, _capsule.transform.position);
        }

    }


    public GameObject timmy;
    public GameObject cheminTimmy;
    private List<Vector3> listNoeud;
    // Use this for initialization
    void Start () {
        //-- le chemin A* de Timmy
        listNoeud = new List<Vector3>();
        foreach (Transform pointChemin in cheminTimmy.transform)
        {
            listNoeud.Add(pointChemin.position);
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
