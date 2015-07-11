﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.UI
{
  using Assets.Scripts.GameLogic;
  using UnityEngine;

  public class BlockDragger : MonoBehaviour
  {
    private bool _dragging;
    private GridController _gridController;
    private Vector3 _startPosition;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    void Awake()
    {
      _gridController = FindObjectOfType<GridController>();
      _collider = GetComponent<BoxCollider2D>();
      _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
      if (!_dragging)
      {
        return;
      }
      var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      mousePos.z = 0;
      transform.position = mousePos;
    }

    void OnMouseDown()
    {
      _dragging = true;
      _spriteRenderer.sortingOrder = 1;
      _startPosition = transform.position;

    }

    void OnMouseUp()
    {
      _dragging = false;
      _spriteRenderer.sortingOrder = 0;
      _collider.enabled = false;
      var hit = Physics2D.Raycast(transform.position, Vector3.down, 10.0f);
      Debug.DrawRay(transform.position, Vector3.down, Color.red, 2.0f);
      _collider.enabled = true;

      if (hit.collider == null)
      {
        
        return;
      }
      //hit.collider.GetComponent<SpriteRenderer>().color = Color.red;

      StartCoroutine(_gridController.TrySwap(_startPosition, gameObject, hit.collider.gameObject));
    }
  }
}
