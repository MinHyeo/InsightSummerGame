using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster 
{
    public class PlayerScanner : MonoBehaviour
    {
        BoxCollider2D scanner;
        Monster monsterScript;

        private void Awake()
        {
            scanner = GetComponent<BoxCollider2D>();
            monsterScript = GetComponentInParent<Monster>();
        }
        public void ScannerInit(Vector2 scannerSize)
        {
            scanner.size = scannerSize;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                monsterScript.FindPlayer(collision);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                monsterScript.LostPlayer();
            }
        }
    }
}