using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour {
  public Movement movement { get; private set; }

  private void Awake() {
    this.movement = GetComponent<Movement>();
  }

  private void Update() {
    if (Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame) {
      this.movement.SetDirection(Vector2.up);
    }
    else if (Keyboard.current.sKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame) {
      this.movement.SetDirection(Vector2.down);
    }
    else if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame) {
      this.movement.SetDirection(Vector2.left);
    }
    else if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame) {
      this.movement.SetDirection(Vector2.right);
    }
    float angle = Mathf.Atan2(this.movement.direction.y, this.movement.direction.x) * Mathf.Rad2Deg;
    this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
  }
}