using UnityEngine;
using UnityEngine.UI;
public class CameraShaker : MonoBehaviour
{
    public Canvas canvas;
    SoundManager sm;

    private float shakeAmount = 0.4f;
    private float shakeTime = 0.0f;
    private Vector3 initialPosition;
    
    void Awake()
    {
        sm = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Start()
    {
        initialPosition = this.transform.position;
    }
    void Update()
    {
        if (shakeTime > 0)
        {
            sm.PlaySound(sm.sounds[3]);
            this.transform.position = Random.insideUnitSphere * shakeAmount + initialPosition;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0.0f;
            this.transform.position = initialPosition;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }
    }

    public void RumpShaker(float time, float amount = 0.4f)
    {
        shakeAmount = amount;
        shakeTime = time;
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.renderMode = RenderMode.WorldSpace;
    }
}
