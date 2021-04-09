using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class LocomotionController : MonoBehaviour
{
    public XRController leftTeleprtRay;

    public XRController RightTeleprtRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (leftTeleprtRay)
        {
            leftTeleprtRay.gameObject.SetActive(CheckIfActivated(leftTeleprtRay));
        }
        if (RightTeleprtRay)
        {
           RightTeleprtRay.gameObject.SetActive(CheckIfActivated(RightTeleprtRay));
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
