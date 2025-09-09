using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> _pickables = new();
    [SerializeField]
    private PlayerController _playerController;
    [SerializeField]
    private ScoreManager _scoreManager;
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

        _scoreManager.SetMaxScore(pickables.Length);
    }

    private void OnPickablePicked(Pickable pickable)
    {
        if (pickable.Type == PickableType.PowerUp)
        {
            _playerController?.PickPowerUp();
        }
        // add score
        if(_scoreManager != null) _scoreManager.AddScore(1);
        
        // remove from list
        _pickables.Remove(pickable);
        // destroying game object
        Destroy(pickable.gameObject);

        if (_pickables.Count <= 0)
        {
            // trigger win
            SceneManager.LoadScene("WinScene");
        }
    }
}
