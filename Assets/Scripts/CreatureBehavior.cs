using UnityEngine;
using UnityEngine.Rendering;

public class CreatureBehavior : MonoBehaviour, IPoolClient
{
    [HideInInspector] public SpawnPoint sp;
    public void Arise(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.SetPositionAndRotation(position, rotation);
    }

    public void Fall()
    {
        gameObject.SetActive(false);
    }

    public void Teleport()
    {
        sp.Teleport(this);
    }

}
