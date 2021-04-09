using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacterstics;
    public List<GameObject> controllerprefab;
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spwnedHandModel;
    public GameObject handModelPrefab;
    public bool showController = false;
    private Animator handAnimator;
    // Start is called before the first frame update
    void Start()
    {
        TryIniti();
    }
    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
    void TryIniti()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(controllerCharacterstics, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerprefab.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("Did not find corresponding controller model");
                Instantiate(controllerprefab[0], transform);
            }
            spwnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spwnedHandModel.GetComponent<Animator>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);
        //if(primaryButtonValue)
        //{
        //    Debug.Log("Primary Button Pressed");
        //}
        //targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        //if (triggerValue > 0.01f)
        //{
        //    Debug.Log("Primary triggerValue Pressed" + triggerValue);
        //}
        //targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue);
        //if (primary2DAxisValue != Vector2.zero)
        //{
        //    Debug.Log("Primary primary2DAxisValue Pressed" + primary2DAxisValue);
        //}
        if (!targetDevice.isValid)
        {
            TryIniti();
        }
        else
        {
            if (showController)
            {
                spwnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spwnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
        
    }
}
