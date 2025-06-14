using UnityEngine;
using NUnit.Framework;
using System.Collections;

public class CarMovementTest : MonoBehaviour
{
    public GameObject car1;
    public GameObject car2;

    void Start()
    {
        StartCoroutine(CarMovesRight(car1));
        StartCoroutine(CarMovesRight(car2));

        StartCoroutine(CarMovesLeft(car1, 1f));
        StartCoroutine(CarMovesLeft(car2, 1f));

        StartCoroutine(CarBoosts(car1, 2f));
        StartCoroutine(CarBoosts(car2, 2f));

        StartCoroutine(CarJumps(car1, 3f));
        StartCoroutine(CarJumps(car2, 3f));

    }

    public IEnumerator CarMovesLeft(GameObject car, float delay = 0f)
    {
        yield return new WaitForSeconds(4f + delay);

        var carMovement = car.GetComponent<CarMovement>();
        Assert.IsNotNull(carMovement, "CarMovement component not found on car1.");

        // Record initial position
        Vector3 initialPosition = car1.transform.position;

        // Simulate left key press
        carMovement.testInputs.leftKeyPressed = true;
        yield return new WaitForSeconds(0.2f);
        carMovement.testInputs.leftKeyPressed = false;

        // Record new position
        Vector3 newPosition = car1.transform.position;

        // Check if the car moved left (X decreased)
        Assert.Less(newPosition.x, initialPosition.x,
            $"Car did not move left as expected. Initial X: {initialPosition.x}, New X: {newPosition.x}");

        // print in console left movement success message
        Debug.Log("CAR MOVED LEFT SUCCESSFULLY!");
    }

    public IEnumerator CarMovesRight(GameObject car, float delay = 0f)
    {
        yield return new WaitForSeconds(4f + delay);

        var carMovement = car.GetComponent<CarMovement>();
        Assert.IsNotNull(carMovement, "CarMovement component not found on car1.");

        // Record initial position
        Vector3 initialPosition = car1.transform.position;

        // Simulate left key press
        carMovement.testInputs.rightKeyPressed = true;
        yield return new WaitForSeconds(0.2f);
        carMovement.testInputs.rightKeyPressed = false;

        // Record new position
        Vector3 newPosition = car1.transform.position;

        Assert.Greater(newPosition.x, initialPosition.x,
            $"Car did not move right as expected. Initial X: {initialPosition.x}, New X: {newPosition.x}");

        // print in console left movement success message
        Debug.Log("CAR MOVED RIGHT SUCCESSFULLY!");
    }

    public IEnumerator CarBoosts(GameObject car, float delay = 0f)
    {
        yield return new WaitForSeconds(4f + delay);

        var carMovement = car.GetComponent<CarMovement>();
        Assert.IsNotNull(carMovement, "CarMovement component not found on car1.");

        // Record initial position
        Vector3 initialPosition = car1.transform.position;

        // Simulate left key press
        carMovement.testInputs.boostKeyPressed = true;
        yield return new WaitForSeconds(0.2f);
        carMovement.testInputs.boostKeyPressed = false;

        // Record new position
        Vector3 newPosition = car1.transform.position;

        if (carMovement.inputConfig.isLeftPlayer)
        {
            Assert.Greater(newPosition.x, initialPosition.x,
                $"Car did not boost. Initial X: {initialPosition.x}, New X: {newPosition.x}");
        }
        else
        {
            Assert.Greater(newPosition.x, initialPosition.x,
                $"Car did not boost. Initial X: {initialPosition.x}, New X: {newPosition.x}");
        }

        // print in console left movement success message
        Debug.Log("CAR BOOSTED SUCCESSFULLY!");
    }

    public IEnumerator CarJumps(GameObject car, float delay = 0f)
    {
        yield return new WaitForSeconds(4f + delay);

        var carMovement = car.GetComponent<CarMovement>();
        Assert.IsNotNull(carMovement, "CarMovement component not found on car1.");

        // Record initial position
        Vector3 initialPosition = car.transform.position;

        // Simulate jump key press
        carMovement.testInputs.jumpKeyPressed = true;
        yield return new WaitForSeconds(0.01f);             // THIS NEEDS TO BE SHORTER THAN A FRAME
        carMovement.testInputs.jumpKeyPressed = false;

        // Wait a short moment for the jump to take effect
        yield return new WaitForSeconds(0.2f);

        // Record new position
        Vector3 newPosition = car.transform.position;

        // Check if the car moved up (Y increased)
        Assert.Greater(newPosition.y, initialPosition.y,
            $"Car did not jump as expected. Initial Y: {initialPosition.y}, New Y: {newPosition.y}");

        Debug.Log("CAR JUMPED SUCCESSFULLY!");
    }
}
