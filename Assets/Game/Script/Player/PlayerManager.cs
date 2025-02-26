using Game.Script.Player.Config;
using Game.Script.Player.Old_Scripts.PlayerFiniteStateMachine;
using Game.Script.Player.PlayerFiniteStateMachine;
using Game.Script.Player.PlayerStates.SubStates;
using Game.Script.Player.PlayerStates.SuperStates;
using UnityEngine;

namespace Game.Script.Player
{
    public class PlayerManager : MonoBehaviour
    {
        #region State Variables
        public PlayerStateMachine StateMachine {get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerJumpState JumpState { get; private set; }
        public PlayerInAirState InAirState { get; private set; }
        public PlayerLandState LandState { get; private set; }
        #endregion
        
        #region Components 
        public Animator Anim { get; private set; }
        public Rigidbody2D Rb { get; private set; }
        public PlayerInputHandler InputHandler { get; private set; }
        [SerializeField]
        private PlayerConfig config;
        #endregion

        #region CheckTransform
        [SerializeField] private Transform groundCheck;
        #endregion
        
        #region Other Variables
        public Vector2 CurrentVelocity { get; private set; }
        public int FacingDirection { get; private set; }
        private Vector2 workSpace;
        #endregion
        
        private void Awake()
        {
            
        }

        public void Initialize()
        {
            StateMachine = new PlayerStateMachine();
            
            IdleState = new PlayerIdleState(this, StateMachine, config, "Idle" );
            MoveState = new PlayerMoveState(this, StateMachine, config, "Move" );
            JumpState = new PlayerJumpState(this, StateMachine, config, "inAir" );
            InAirState = new PlayerInAirState(this, StateMachine, config, "inAir" );
            LandState = new PlayerLandState(this, StateMachine, config, "land" );
            
            Anim = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandler>();
            Rb = GetComponent<Rigidbody2D>();
            
            FacingDirection = 1;
            
            StateMachine.Initialize(IdleState);
        }

        #region Unity Callbacks Functions
        private void Update()
        {
            CurrentVelocity = Rb.velocity;
            StateMachine.CurrentState.LogicUpdate();
            
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
        #endregion

        #region Set Functions
        public void SetVelocityX(float velocity)
        {
            workSpace.Set(velocity, CurrentVelocity.y);   
            Rb.velocity = workSpace;
            CurrentVelocity = workSpace;
        }

        public void SetVelocityY(float velocity)
        {
            workSpace.Set(CurrentVelocity.x, velocity);
            Rb.velocity = workSpace;
            CurrentVelocity = workSpace;
        }

        #endregion

        #region Check Functions

        public bool CheckIfGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, config.groundCheckRadius, config.whatIsGround);
        }

        public void CheckIfShouldFlip(int xInput)
        {
            if (xInput != 0 && xInput != FacingDirection)
            {
                FlipPlayer();
            }
        }
        

        #endregion

        #region Other Functions
        
        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
        
        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
        private void FlipPlayer()
        {
            FacingDirection *= -1;
            transform.Rotate(0f, 180f, 0f);
        }

        #endregion
    }
}
