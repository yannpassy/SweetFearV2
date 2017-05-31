using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimmyMove : MonoBehaviour
{
    public GameObject timmy;
    public GameObject perso;
    public GameObject circuitTimmy;

    private List<Vector3> circuit;
    private int indexCircuit;
    private List<Vector3> cheminRetour;
    private int indexCheminRetour;
    private Vector3 directionTimmy;
    private Vector3 localisationPerso;
    private enum etat { patrouille, poursuite, retourPatrouille };
    private etat etatTimmy;
    private float champDeVision;
    private float pointCheminApproximation;
	public float vitesse;
    private float rotationSpeed;

    // Use this for initialization
    void Start()
    {
        //-- le circuit de Timmy
        circuit = new List<Vector3>();

        foreach (Transform pointCircuit in circuitTimmy.transform)
        {
            circuit.Add(pointCircuit.position);
        }


        cheminRetour = new List<Vector3>();

        champDeVision = 5.0f;
        pointCheminApproximation = 0.5f;
        vitesse = 0.4f;

        rotationSpeed = 50.0f;
        etatTimmy = etat.patrouille;
        indexCircuit = 0;
        indexCheminRetour = 0;
        timmy.transform.LookAt(circuit[indexCircuit]);
        directionTimmy = Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {

        //deplacement de Timmy
        timmy.transform.Translate(directionTimmy.normalized * Time.deltaTime * vitesse);

        //--         Etat patrouille        -- //
        if (etatTimmy == etat.patrouille)
        {
            //si Suzy se trouve dans le périmètre de Timmy
            if (Vector3.Distance(timmy.transform.position, perso.transform.position) < champDeVision)
            {
                localisationPerso = perso.transform.position;
                cheminRetour.Add(timmy.transform.position);
                timmy.transform.LookAt(perso.transform.position);
                etatTimmy = etat.poursuite;
            }

            // Timmy va au prochain point si il ne trouve personne
            else if (Vector3.Distance(timmy.transform.position, circuit[indexCircuit]) < pointCheminApproximation)
            {
                indexCircuit++;
                if (indexCircuit >= circuit.Count) { indexCircuit = 0; }
                timmy.transform.LookAt(circuit[indexCircuit]);
                etatTimmy = etat.patrouille;
            }
        }

        //--         Etat poursuite         --//
        else if (etatTimmy == etat.poursuite)
        {
            if (Vector3.Distance(timmy.transform.position, localisationPerso) < pointCheminApproximation)
            {
                //si Suzy se trouve encore dans le périmètre de Timmy
                if (Vector3.Distance(timmy.transform.position, perso.transform.position) < champDeVision)
                {
                    localisationPerso = perso.transform.position;
                    cheminRetour.Add(timmy.transform.position);
                    timmy.transform.LookAt(perso.transform.position);
                    etatTimmy = etat.poursuite;
                }
                // si Timmy ne voit plus Suzy, il revient au parcours
                else
                {
                    indexCheminRetour = cheminRetour.Count - 1;
                    timmy.transform.LookAt(cheminRetour[indexCheminRetour]);
                    etatTimmy = etat.retourPatrouille;
                }
            }

        }

        //--        Etat retourPatrouille          --//
        if (etatTimmy == etat.retourPatrouille)
        {
            if (Vector3.Distance(timmy.transform.position, cheminRetour[indexCheminRetour]) < pointCheminApproximation)
            {
                cheminRetour.RemoveAt(indexCheminRetour);
                //si Suzy se trouve dans le périmètre de Timmy
                if (Vector3.Distance(timmy.transform.position, perso.transform.position) < champDeVision)
                {
                    localisationPerso = perso.transform.position;
                    cheminRetour.Add(timmy.transform.position);
                    timmy.transform.LookAt(perso.transform.position);
                    etatTimmy = etat.poursuite;
                }
                // Timmy va vers  le parcours
                else if (indexCheminRetour - 1 >= 0)
                {
                    indexCheminRetour--;
                    timmy.transform.LookAt(cheminRetour[indexCheminRetour]);
                    etatTimmy = etat.retourPatrouille;
                }
                // Timmy retourne sur le circuit
                else
                {
                    timmy.transform.LookAt(circuit[indexCircuit]);
                    etatTimmy = etat.patrouille;
                }
            }
        }

    }

    // si Timmy tombe sur un obstacle
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag != "terrain" && hit.gameObject.tag != "Player" && hit.gameObject.tag != "Untagged" && hit.gameObject.tag != "obstacle")
        {
            indexCheminRetour = cheminRetour.Count - 1;
            timmy.transform.LookAt(cheminRetour[indexCheminRetour]);
            etatTimmy = etat.retourPatrouille;
        }
    }
}
