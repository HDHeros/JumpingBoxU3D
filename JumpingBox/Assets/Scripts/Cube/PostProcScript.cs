using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcScript : MonoBehaviour
{
    private PostProcessVolume _ppVol;
    private ChromaticAberration _chromatic;

    private void Start()
    {
        _ppVol = GameObject.FindGameObjectWithTag("PPVol").GetComponent<PostProcessVolume>();
        _ppVol.profile.TryGetSettings(out _chromatic);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<PlatformSolid>())
        {
            _chromatic.intensity.value = 0.5f;
            StartCoroutine(TurnOffChromatic());
        }

    }

    IEnumerator TurnOffChromatic()
    {
        yield return new WaitForSeconds(0.1f);
        _chromatic.intensity.value = 0f;
    }
}
