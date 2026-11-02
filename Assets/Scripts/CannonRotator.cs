using UnityEngine;
using UnityEngine.InputSystem;

// Rotates the cannon bone (Bone.001) with the left/right arrow keys.
// Put this script on the ship object (battleship-2) in the scene.
public class CannonRotator : MonoBehaviour
{
    [SerializeField] private string boneName = "Bone.001";
    [SerializeField] private float degreesPerSecond = 45f;

    // Axis in the bone's own space. If the cannon turns the wrong way
    // (tilts instead of turning), change this in the Inspector, e.g. to (0,0,1).
    [SerializeField] private Vector3 rotationAxis = Vector3.up;

    private Transform cannonBone;

    private void Awake()
    {
        cannonBone = FindChildRecursive(transform, boneName);
        if (cannonBone == null)
        {
            Debug.LogError($"CannonRotator: bone '{boneName}' not found under {name}.");
            enabled = false;
        }
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        float direction = 0f;
        if (keyboard.leftArrowKey.isPressed) direction -= 1f;
        if (keyboard.rightArrowKey.isPressed) direction += 1f;
        if (direction == 0f) return;

        cannonBone.Rotate(rotationAxis, direction * degreesPerSecond * Time.deltaTime, Space.Self);
    }

    private static Transform FindChildRecursive(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName) return child;
            Transform found = FindChildRecursive(child, childName);
            if (found != null) return found;
        }
        return null;
    }
}
