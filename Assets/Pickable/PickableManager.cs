using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickables = new();
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
        _pickables.Remove(pickable);
        Destroy(pickable.gameObject);
        Debug.Log("Pickable List: " + _pickables.Count);
        if (_pickables.Count <= 0)
        {
            Debug.Log("Win");
        }
    }
}
