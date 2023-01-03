using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayersUi : MonoBehaviour
{
    public TextMeshProUGUI currentPoints;
    public TextMeshProUGUI PathWarning;
    public PlayerMovement player;
    [SerializeField] private PointsObject pointsObject;
    [SerializeField] private PathModel pathModel;

    private bool _isActive;

    private void Awake()
    {
        currentPoints.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        currentPoints.text = "Points: " + pointsObject.GetPoints();
    }

    public void Warning()
    {
        if (_isActive)
        {
            return;
        }

        _isActive = true;
        StartCoroutine(Countdown());
        PathWarning.gameObject.SetActive(true);
    }
    
    private IEnumerator Countdown()
    {
        float duration = 3f; // 3 seconds you can change this 
        float normalizedTime = 0;
        while(normalizedTime <= 1f)
        {
            PathWarning.text = $"The second Path disappears in {3f - normalizedTime} seconds"  ;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        PathWarning.gameObject.SetActive(false);
        _isActive = false;
        pathModel.DestroyPath();
    }
}
