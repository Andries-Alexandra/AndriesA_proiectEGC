using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    //distanta maxima la care se poate detecta o planeta
    public float detectionRange = 1000f;

    private string currentPlanetName = ""; //memoreaza numele planetei curente
    private bool isLookingAtPlanet = false; //indica daca privim o planeta

    //verifica in fiecare frame daca privim o planeta
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * detectionRange, Color.red);

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            currentPlanetName = hit.transform.name;
            isLookingAtPlanet = true;
        }
        else
        {
            isLookingAtPlanet = false;
        }
    }

    //afiseaza informatiile pe ecran
    void OnGUI()
    {
        if (isLookingAtPlanet)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 40; 
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;

            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100), currentPlanetName, style);

            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height - 100, 300, 50), "Distanta: " + (int)Vector3.Distance(transform.position, transform.position), style);
        }
    }
}