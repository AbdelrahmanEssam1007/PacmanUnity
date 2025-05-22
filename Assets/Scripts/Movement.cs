using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour {
  public float speed = 8f;
  public float speedMultiplier = 1.0f;
  public Vector2 initDirection;
  public LayerMask collisionMask;
  public Rigidbody2D rigidBody { get; private set; }
  public Vector2 direction { get; private set; }
  public Vector2 nextDirection { get; private set; }
  public Vector3 startingPosition { get; private set; }


  private void Awake() {
    this.rigidBody = GetComponent<Rigidbody2D>();
    this.startingPosition = this.transform.position;
  }

  private void Update() {
    if (this.nextDirection != Vector2.zero) {
      SetDirection(this.nextDirection);
    }
  }

  private void Start() {
    this.direction = this.initDirection;
    this.nextDirection = this.initDirection;
  }

  public void ResetState() {
    this.speedMultiplier = 1.0f;
    this.direction = this.initDirection;
    this.nextDirection = Vector2.zero;
    this.transform.position = this.startingPosition;
    this.rigidBody.bodyType = RigidbodyType2D.Dynamic;
    this.enabled = true;
  }
  private void FixedUpdate() {
    Vector2 position = this.rigidBody.position;
    Vector2 translate = this.direction * (this.speed * this.speedMultiplier * Time.fixedDeltaTime);
    this.rigidBody.MovePosition(position + translate);
  }
  
  public void SetDirection(Vector2 newDirection, bool force = false) {
    if (force || !occupied(newDirection)) {
      this.direction = newDirection;
      this.nextDirection = Vector2.zero;
    }
    else {
      this.nextDirection = newDirection;
    }
  }
  
  public bool occupied(Vector2 newDirection) {
    RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * .75f, 0f, newDirection, 1.5f, this.collisionMask);
    return hit.collider != null;
  }
}