using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayersUi : MonoBehaviour
{
    public TextMeshProUGUI currentPoints;
    public PlayerMovement player;
    [SerializeField] private PointsObject pointsObject;

    private void Awake()
    {
        currentPoints.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        currentPoints.text = "Points: " + pointsObject.GetPoints();
    }
}
