using UnityEngine;

namespace Game.Script.Player.Config
{
    [CreateAssetMenu(fileName = "New Player Config", menuName = "Config/Player Config/Base Config")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("MoveState")]
        public float movementVelocity = 10;
        
        [Header("JumpState")]
        public float jumpVelocity = 15f;
        public int amountOfJumps = 1;
        public float variableJumpHeightMultiplier = 0.5f;
        
        [Header("Check Variables")]
        public float groundCheckRadius = 0.3f;
        public LayerMask whatIsGround;
    }
}
