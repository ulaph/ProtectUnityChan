using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class SteamVRThrowObject : MonoBehaviour
{
    [SerializeField] GameObject uni;
    [SerializeField] GameObject cube;
    [SerializeField] Rigidbody attachPoint;

    SteamVR_TrackedObject trackedObj;
    FixedJoint joint;
    int objMode;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        objMode = 0;
        cube.transform.localScale = Vector3.one * 0.5F;
    }

    void Update()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            modeChange(objMode);
        }
        if (joint == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            var go = GameObject.Instantiate(objSelect(objMode));
            go.SetActive(true);
            go.transform.position = attachPoint.transform.position;

            joint = go.AddComponent<FixedJoint>();
            joint.connectedBody = attachPoint;
        }
        else if (joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            var go = joint.gameObject;
            var rigidbody = go.GetComponent<Rigidbody>();
            Object.DestroyImmediate(joint);
            joint = null;
            Object.Destroy(go, 15.0f);

            // We should probably apply the offset between trackedObj.transform.position
            // and device.transform.pos to insert into the physics sim at the correct
            // location, however, we would then want to predict ahead the visual representation
            // by the same amount we are predicting our render poses.

            var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
            if (origin != null)
            {
                rigidbody.velocity = origin.TransformVector(device.velocity);
                rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
                rigidbody.velocity = rigidbody.velocity * 1.5F;
            }
            else
            {
                rigidbody.velocity = device.velocity;
                rigidbody.angularVelocity = device.angularVelocity;
            }

            rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            if (objMode == 0)
            {
                device.TriggerHapticPulse(100);
            }
        }
    }

    void modeChange(int nowMode)
    {
        switch (nowMode)
        {
            case 0:
                objMode = 1;
                break;
            case 1:
                objMode = 0;
                break;
        }
    }

    GameObject objSelect(int nowMode)
    {
        switch (nowMode)
        {
            case 0:
                return uni;
            case 1:
                return cube;
            default:
                return uni;
        }
    }
}

