using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickables = new();
    [SerializeField]
    private PlayerController _playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitPowerUps();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitPowerUps()
    {
        Pickable[] pickables = FindObjectsByType<Pickable>(FindObjectsSortMode.None);

        foreach (Pickable pickable in pickables)
        {
            _pickables.Add(pickable);
            pickable.OnPicked += OnPickablePicked;
        }

        Debug.Log("Initialized " + _pickables.Count + " pickables.");
    }

    private void OnPickablePicked(Pickable pickable)
    {
        if (pickable.Type == PickableType.PowerUp)
        {
            _playerController?.PickPowerUp();
        }
        _pickables.Remove(pickable);
        Destroy(pickable.gameObject);
        if (_pickables.Count <= 0)
        {
            Debug.Log("Win");
        }
    }
}
