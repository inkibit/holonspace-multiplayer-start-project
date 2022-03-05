using UnityEngine;
using UnityEngine.Events;

public class Triggered : MonoBehaviour
{
    public UnityEvent TriggerEnter;
    public UnityEvent TriggerExit;
    [SerializeField] float cooldown = 0;
    private float counter;
    private bool onCooldown = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (onCooldown) return;
        TriggerEnter.Invoke();
        onCooldown = true;
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit.Invoke();
    }

    private void Update()
    {
        if (onCooldown)
        {
            counter += Time.deltaTime;
            if(counter >= cooldown)
            {
                onCooldown = false;
                counter = 0;
            }
        }
    }
}
