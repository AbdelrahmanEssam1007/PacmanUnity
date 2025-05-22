using System;
using UnityEngine;

public class Ghost : MonoBehaviour {
  public Movement movement { get; private set; }
  public GhostHome home { get; private set; }
  public GhostChase chase { get; private set; }
  public GhostScared scared { get; private set; }
  public GhostScatter scatter { get; private set; }
  public GhostBehaviour initBehaviour;
  public Transform target;
  public int m_GhostPoints = 200;

  private void Awake() {
    this.movement = GetComponent<Movement>();
    this.home = GetComponent<GhostHome>();
    this.chase = GetComponent<GhostChase>();
    this.scared = GetComponent<GhostScared>();
    this.scatter = GetComponent<GhostScatter>();
  }

  public void Start() {
    ResetState();
  }

  public void ResetState() {
    this.gameObject.SetActive(true);
    this.movement.ResetState();
    this.scared.Disable();
    this.chase.Disable();
    this.scatter.Enable();
    if (this.home != this.initBehaviour) {
      this.home.Disable();
    }

    if (this.initBehaviour != null) {
      this.initBehaviour.Enable();
    }
  }

  private void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.layer == LayerMask.NameToLayer("Pacman")) {
      if (this.scared.enabled) {
        FindFirstObjectByType<GameManager>().EatGhost(this);
      }
      else {
        FindFirstObjectByType<GameManager>().EatPacman();
      }
   }
  }
}