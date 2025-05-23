using UnityEngine;

public class GhostChase : GhostBehaviour {
  private void OnDisable() {
    if (this.ghost != null && this.ghost.scatter != null) {
      this.ghost.scatter.Enable();
    }
  }

  private void OnTriggerEnter2D(Collider2D other) {
    Node node = other.GetComponent<Node>();
    if (node != null && this.enabled && !this.ghost.scared.enabled) {
      float closestDistance = float.MaxValue;
      Vector2 direction = Vector2.zero;
      foreach ( Vector2 path in node.paths) {
        Vector3 newPos = this.transform.position + new Vector3(path.x, path.y, 0);
        float distance = (this.ghost.target.position - newPos).sqrMagnitude;
        if (distance < closestDistance) {
          closestDistance = distance;
          direction = path;
        }
      }
      this.ghost.movement.SetDirection(direction);
    }
  }
}