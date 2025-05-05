using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoalTrigger : MonoBehaviour
{
    // Referință la sistemul de scor
    public GoalSystem goalSystem;

    // Determină dacă aceasta este poarta din stânga sau din dreapta
    public bool isLeftGoal = true;

    // Tag-ul mingii pentru verificare (implicit "Ball")
    public string ballTag = "Ball";

    // Opțional: Efect vizual pentru poartă
    public GameObject goalVisualEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificăm dacă obiectul care a intrat în trigger este mingea
        if (collision.CompareTag(ballTag))
        {
            // Dacă este poarta din stânga, echipa din dreapta a marcat
            // Dacă este poarta din dreapta, echipa din stânga a marcat
            if (isLeftGoal)
            {
                if (goalSystem != null)
                    goalSystem.GoalScoredLeft();
            }
            else
            {
                if (goalSystem != null)
                    goalSystem.GoalScoredRight();
            }

            // Activăm efectul vizual dacă există
            if (goalVisualEffect != null)
            {
                // Clonăm efectul pentru a-l putea folosi de mai multe ori
                GameObject effect = Instantiate(goalVisualEffect, collision.transform.position, Quaternion.identity);
                Destroy(effect, 2f); // Distrugem efectul după 2 secunde
            }
        }
    }
}