using UnityEngine;

public class AntiTeleport : MonoBehaviour
{
    public GameObject leftHandRayInteractor;
    public GameObject rightHandRayInteractor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("RightHand"))
        {
            rightHandRayInteractor.SetActive(false);
        }
        if (other.name.Contains("LeftHand"))
        {
            leftHandRayInteractor.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("RightHand"))
        {
            rightHandRayInteractor.SetActive(true);
        }
        if (other.name.Contains("LeftHand"))
        {
            leftHandRayInteractor.SetActive(true);
        }
    }
}
