using UnityEngine;

public class LookAtXZOnly : MonoBehaviour
{
    [SerializeField] private Transform realTarget;
    [SerializeField] private float fixedY = 0f;

    void LateUpdate()
    {
        Vector3 pos = realTarget.position;
        pos.y = fixedY;
        transform.position = pos;
    }
}