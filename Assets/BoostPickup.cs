using UnityEngine;

public class BoostPickup : MonoBehaviour
{
    float boostAmount;
    private void OnTriggerEnter2D(Collider2D other)
    {
        CarMovement car = other.GetComponent<CarMovement>();
        if (car != null)
        {
            // If the car is already at max boost, do not add more
            if (car.remainingBoost >= 100f)
            {
                return;
            }

            car.remainingBoost += boostAmount;
            car.remainingBoost = Mathf.Min(car.remainingBoost, 100f);
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        boostAmount = Random.Range(20f, 70f);

        float scale = Mathf.Min((boostAmount / 100) + 0.3f, 100);
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}
