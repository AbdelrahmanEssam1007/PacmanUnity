using System;
using UnityEngine;

public class Pellet : MonoBehaviour {
  public int points = 10;

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
      EatPellet();
    }
  }

  protected virtual void EatPellet() {
    this.gameObject.SetActive(false);
  }
}