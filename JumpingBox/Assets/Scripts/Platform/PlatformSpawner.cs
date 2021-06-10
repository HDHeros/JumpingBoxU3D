using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _platform;
    private Transform _transform;


    private void Start()
    {
        _transform = GetComponent<Transform>();
        StartCreate();
    }
    private void Update()
    {
    }

    IEnumerator CreatePlatform()
    {
        Vector3 position = new Vector3(Random.Range(-2.5f, 2.5f), _transform.position.y, Random.Range(-2.5f, 2.5f));
        GameObject newPlatform =  Instantiate(_platform);
        newPlatform.transform.position = position;
        yield return new WaitForSeconds(1);
        StartCreate();
    }
    private void StartCreate()
    {
        //StartCoroutine(CreatePlatform());
    }
}
