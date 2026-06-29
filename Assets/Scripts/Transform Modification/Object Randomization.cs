using UnityEngine;

public class ObjectRandomization : MonoBehaviour {

    public Vector3 minRange;

    public Vector3 maxRange;

    public float minRotationRange = 0;

    public float maxRotationRange = 180;






    private void Awake() {
        RandomizePosition();
        RandomizeRotation();
    }


    void RandomizePosition() {
        float x = Random.Range(minRange.x, maxRange.x);
        float y = Random.Range(minRange.y, maxRange.y);
        float z = Random.Range(minRange.z, maxRange.z);
        transform.position = new Vector3(x, y, z);
    }

    void RandomizeRotation() {
        float randomX, randomY, randomZ;
        randomY = randomX = randomZ = Random.Range(minRotationRange, maxRotationRange);
        transform.rotation = Quaternion.Euler(randomX, randomY, randomZ);
    }

}
