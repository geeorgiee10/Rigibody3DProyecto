using UnityEngine;

public class WindFXController : MonoBehaviour
{
    public ParticleSystem vientoFX;
    private ParticleSystem.VelocityOverLifetimeModule velocityModule;

    [Header("Audio")]
    [SerializeField] private AudioSource windAudio;
    [SerializeField] private float maxWindVolume = 1f;

    void Start()
    {
        velocityModule = vientoFX.velocityOverLifetime;
    }

    void Update()
    {
        if (LevelManager.Instance == null) return;

        Vector3 viento = LevelManager.Instance.VientoActual;

        bool hayViento = viento != Vector3.zero;

        if (hayViento)
        {
            if (!vientoFX.isPlaying)
                vientoFX.Play();

            // aplicar dirección del viento
            velocityModule.x = viento.x;
            velocityModule.y = viento.y;
            velocityModule.z = viento.z;

            if (windAudio != null)
            {
                if (!windAudio.isPlaying)
                    windAudio.Play();

                float intensidad = viento.magnitude;
                windAudio.volume = Mathf.Clamp01(intensidad) * maxWindVolume;
            }
        }
        else
        {
            if (vientoFX.isPlaying)
                vientoFX.Stop();

            if (windAudio != null)
            {
                windAudio.volume = Mathf.Lerp(windAudio.volume, 0f, Time.deltaTime * 2f);

                if (windAudio.volume < 0.01f)
                    windAudio.Stop();
            }
        }
    }
}