using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIController : MonoBehaviour
{
    [SerializeField] private Button _rightMoveBtn;
    [SerializeField] private Button _leftMoveBtn;
    [SerializeField] private Button _jumpBtn;
    [SerializeField] private Button _fireBtn;
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private TextMeshProUGUI _patronText;
    private void Awake()
    {
        
    }
}
