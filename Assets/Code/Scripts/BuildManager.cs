using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;
    
    [Header("References")]
    [SerializeField] private GameObject[] turretPrefabs;

    private int selectedTurret = 0;

    private void Awake() {
        main = this;
    }

    public GameObject GetSelectedTurret() {
        return turretPrefabs[selectedTurret];
    }
}
