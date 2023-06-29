using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintsCreator : MonoBehaviour
{
    [SerializeField] private GameObject footPrintEffect;
    [SerializeField] private GameObject leftFootPrint;
    [SerializeField] private GameObject rightFootPrint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayDist = 0.01f;
    [SerializeField] private float timeToNextFootPrint = 0.5f;
    [SerializeField] private float timeLastLeftFootPrint = 0;
    [SerializeField] private float timeLastRightFootPrint = 0;

    private bool _isLeftFoot = true;
    private bool _isLeftSpawned = true;
    private bool _isRightSpawned = true;
    void Update()
    {
        CheckGroundUnderFoot();
        
        if (timeLastLeftFootPrint >= timeToNextFootPrint)
        { }
        else
        {
            timeLastLeftFootPrint += Time.deltaTime;
        }
        if (timeLastRightFootPrint >= timeToNextFootPrint)
        { }
        else
        {
            timeLastRightFootPrint += Time.deltaTime;
        }
    }

    private void CheckGroundUnderFoot()
    {
        if (_isLeftFoot)
        {
            if (!Physics.Raycast(leftFootPrint.transform.position, Vector3.down, out var hit, rayDist, groundLayer))
            {
                _isLeftSpawned = true;
                _isLeftFoot = false;
                return;
            }
            
            if (_isLeftSpawned && timeLastLeftFootPrint >= timeToNextFootPrint)
            {
                _isLeftSpawned = false;
                SpawnFootPrintEffect(leftFootPrint.transform.position);
                Debug.Log("leftFootPrint");

                timeLastLeftFootPrint = 0;
            }
        }
        else
        {
            if (!Physics.Raycast(rightFootPrint.transform.position, Vector3.down, out var hit, rayDist, groundLayer))
            {
                _isRightSpawned = true;
                _isLeftFoot = true;
                return;
            }

            if (_isRightSpawned && timeLastRightFootPrint >= timeToNextFootPrint)
            {
                _isRightSpawned = false;
                SpawnFootPrintEffect(rightFootPrint.transform.position);
                Debug.Log("rightFootPrint");

                timeLastRightFootPrint = 0;
            }
        }
    }

    private void SpawnFootPrintEffect(Vector3 position)
    {
        var effect = Instantiate(footPrintEffect, DynamicManager.Instance.transform);
        effect.transform.position = position;
    }
}
