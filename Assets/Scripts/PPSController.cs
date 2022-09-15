using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPSController : MonoBehaviour
{
    [SerializeField] private PostProcessVolume PPS;
    private ColorGrading CG;
    private Vignette VG;

    public float value = 1;

    float maxHP = 7;
    float HP = 7;
    private void OnEnable()
    {
        PPS = GetComponent<PostProcessVolume>();
        PPS.profile.TryGetSettings(out CG);
        PPS.profile.TryGetSettings(out VG);
        EventManager.Subscribe(eEventType.PlayerDamag, (arg) => ChangeRed());

    }

    private void ChangeRed()
    {
        value = 0.1f;
        HP -= 1;

    }

    private void Update()
    {
         if (value >= Mathf.Lerp(0.7f, 1f, HP / maxHP))
        return;

        value += Time.deltaTime;
        CG.mixerRedOutGreenIn.value = (1f - value) * 140;
        VG.intensity.value = (1f - value) * 0.9f;
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(eEventType.PlayerDamag, (arg) => ChangeRed());
    }
}
