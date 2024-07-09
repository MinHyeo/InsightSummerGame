using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface IAttackable {
        public void Attack();
        public void DetectAttack();
        public void CheckAttackRange();
}

interface IChacer {
    public void Chace();
    public void PlayerDetect();
}


