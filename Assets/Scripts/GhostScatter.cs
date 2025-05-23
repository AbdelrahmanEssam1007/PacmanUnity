using UnityEngine;
using Random = UnityEngine.Random;

public class GhostScatter : GhostBehaviour {
  private void OnDisable() {
    if (this.ghost != null && this.ghost.chase != null) {
      this.ghost.chase.Enable();
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    Node node = other.GetComponent<Node>();
    if (node != null && this.enabled && !this.ghost.scared.enabled) {
      int idx = Random.Range(0, node.paths.Count);
      if (node.paths[idx] == -this.ghost.movement.direction && node.paths.Count > 1) {
        idx++;
        idx %= node.paths.Count;
      }
      this.ghost.movement.SetDirection(node.paths[idx]);
    }
  }
}