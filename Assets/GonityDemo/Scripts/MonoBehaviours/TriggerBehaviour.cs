using System.Collections.Generic;
using UnityEngine;

public class TriggerBehaviour : MonoBehaviour
{
    private List<Collider> _colliders = new List<Collider>();

    public bool isColliding(GameObject gameObject)
    {
        return _colliders.Find(c => c.gameObject == gameObject) != null;
    }

    void OnTriggerEnter(Collider other)
    {
        _colliders.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (isColliding(other.gameObject))
        {
            _colliders.Remove(other);
        }
    }
}
