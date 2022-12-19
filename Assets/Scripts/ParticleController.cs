using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem explotionParticle;
    [SerializeField] ParticleSystem fireParticle;
    void Start()
    {
        InputController.singleton.ñlickAction += Fire;
        PlayerHealth.singleton.DeadAction += Explotion;
    }

    private void Explotion()
    {
        explotionParticle.Play();
    }

    private void Fire(bool Value)
    {
        if (Value)
        {
            fireParticle.Play();
        }
        else
        {
            fireParticle.Stop();
        }
    }
}
