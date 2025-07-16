using UnityEngine;
using UnityEngine.UI;

public class AllyUIController : MonoBehaviour
{
    [SerializeField] private Text _diamondText;
    [SerializeField] private Button _spawnButton;

    private PickableCollector _pickableCollector;
    private AllySpawner _allySpawner;


    private void OnEnable()
    {
        _pickableCollector = GameManager.Instance.LevelManager.Player.GetComponent<PickableCollector>();
        _allySpawner = GameManager.Instance.LevelManager.Player.GetComponent<AllySpawner>();

        _spawnButton.onClick.AddListener(() => OnClickSpawnAlly());
    }

    private void LateUpdate()
    {
        if (_pickableCollector)
        {
            int current = _pickableCollector.CurrentDiamonds;
            int required = _pickableCollector.GetRequiredDiamondsForNextAlly();

            _diamondText.text = $"💎 {current} / {required}";
            _spawnButton.interactable = current >= required;
        }
        else
        {
            Debug.LogError("No PickableCollector on Player Prefab");
        }
    }

    public void OnClickSpawnAlly()
    {
        int cost = _pickableCollector.GetRequiredDiamondsForNextAlly();

        if (_pickableCollector.TrySpendDiamonds(cost))
        {
            _pickableCollector.IncrementAllyCount();
            _allySpawner.SpawnAlly();
        }
    }
}
