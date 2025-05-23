using System;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
  public List<Vector2> paths { get; private set; }
  public LayerMask collisionMask;
  private void Start() {
    this.paths = new List<Vector2>();
    this.CheckForPath(Vector2.up);
    this.CheckForPath(Vector2.down);
    this.CheckForPath(Vector2.left);
    this.CheckForPath(Vector2.right);
  }

  private void CheckForPath(Vector2 path) {
    RaycastHit2D hit =
      Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, path, 1.0f, this.collisionMask);
    if (hit.collider == null) {
      this.paths.Add(path);
    }
  }
}
