using System;
using UnityEngine;

public enum PickableType
{
    Coin,
    PowerUp
}

public class Pickable : MonoBehaviour
{
    [SerializeField]
    private PickableType _pickableType;
    public Action<Pickable> OnPicked;

    public PickableType Type => _pickableType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPicked?.Invoke(this);
        }
    }
}
