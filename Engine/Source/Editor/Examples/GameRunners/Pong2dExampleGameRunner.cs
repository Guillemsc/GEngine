using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Components.Objects;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Games.Core;
using GEngine.Modules.Physics2d.Enums;
using GEngine.Utils.Extensions;
using GEngine.Utils.Randomization.Generators;
using GEngine.Utils.Tasks.Runners;
using GEngine.Utils.Time.Timers;
using Raylib_cs;

namespace GEngine.Editor.Examples.GameRunners;

public sealed class Pong2dExampleGameRunner : GameRunner
{
    static readonly Color BackgroundColor = Color.BLACK;
    static readonly Color FieldColor = Color.WHITE;
    
    static readonly Vector2 FieldSize = new(1000f, 600f);
    static readonly Vector2 HalfFieldSize = FieldSize * 0.5f;
    static readonly float FieldThickness = 10f;
    
    static readonly float FieldCameraBoundsMultiplier = 1.3f;
    static readonly Vector2 FieldCameraBounds = FieldSize * FieldCameraBoundsMultiplier;

    static readonly Vector2 PlayerSize = new(10f, 100f);
    static readonly Vector2 PlayerHalfSize = PlayerSize * 0.5f;
    static readonly float PlayerHorizontalPositionFieldSizeMultiplier = 0.9f;
    static readonly float PlayersSpeed = 500f;
    static readonly float PlayerMaxVerticalPositionFieldSizeMultiplier = 0.8f;
    
    static readonly float BallRadius = 11f;
    static readonly float BallSpeed = 300f;
    static readonly float BallBounceOnPlayerAngleRange = 70f;
    static readonly float BallBounceOnPlayerAngleHalfRange = BallBounceOnPlayerAngleRange * 0.5f;

    readonly IRandomGenerator _randomGenerator;
    readonly IAsyncTaskRunner _asyncTaskRunner;
    
    Camera2dComponent? _camera2dComponent;
    WorldEntity? _topWall;
    WorldEntity? _bottomWall;
    WorldEntity? _leftWall;
    WorldEntity? _rightWall;
    WorldEntity? _leftPlayer;
    WorldEntity? _rightPlayer;
    WorldEntity? _ball;

    Vector2 _ballDirection;
    bool _ballCanMove;
    
    public Pong2dExampleGameRunner(IREngineInteractor engine) : base(engine)
    {
        _randomGenerator = new SeedRandomGenerator(0);
        _asyncTaskRunner = new AsyncTaskRunner();
    }

    public override void Start()
    {
        CreateCamera();
        CreateField();
        CreatePlayers();
        CreateBall();
        
        _asyncTaskRunner.Run(StartRound);
    }

    public override void Tick()
    {
        UpdateCamera();
        UpdatePlayersPosition();
        UpdateBallPosition();
    }

    public override void Dispose()
    {
        _asyncTaskRunner.CancelForever();
    }

    void CreateCamera()
    {
        WorldEntity cameraEntity = Engine.Entities.CreateWorld("Camera");
        _camera2dComponent = cameraEntity.AddComponent<Camera2dComponent>();
        
        _camera2dComponent!.SetClearColor(BackgroundColor);
        _camera2dComponent!.FitBoundsToScreen(FieldCameraBounds);
    }

    void CreateField()
    {
        WorldEntity fieldEntity = Engine.Entities.CreateWorld("Field");
        
        _leftWall = Engine.Entities.CreateWorld("LeftWall", fieldEntity);
        _rightWall = Engine.Entities.CreateWorld("RightWall", fieldEntity);
        _topWall = Engine.Entities.CreateWorld("TopWall", fieldEntity);
        _bottomWall = Engine.Entities.CreateWorld("BottomWall", fieldEntity);

        _leftWall.Transform.SetLocalPositionXY(new Vector2(-HalfFieldSize.X, 0));
        BoxRenderer2dComponent leftWallRenderer = _leftWall.AddComponent<BoxRenderer2dComponent>();
        leftWallRenderer.SetSize(new Vector2(FieldThickness, FieldSize.Y + FieldThickness));
        leftWallRenderer.SetColor(FieldColor);
        PhysicsBody2dComponent leftWallBody = _leftWall.AddComponent<PhysicsBody2dComponent>();
        leftWallBody.SetType(PhysicsBody2dType.Kinematic);
        leftWallBody.CreateBox(leftWallRenderer.Size, true);
        
        _rightWall.Transform.SetLocalPositionXY(new Vector2(HalfFieldSize.X, 0));
        BoxRenderer2dComponent rightWallRenderer = _rightWall.AddComponent<BoxRenderer2dComponent>();
        rightWallRenderer.SetSize(new Vector2(FieldThickness, FieldSize.Y + FieldThickness));
        rightWallRenderer.SetColor(FieldColor);
        PhysicsBody2dComponent rightWallBody = _rightWall.AddComponent<PhysicsBody2dComponent>();
        rightWallBody.SetType(PhysicsBody2dType.Kinematic);
        rightWallBody.CreateBox(rightWallRenderer.Size, true);
        
        _topWall.Transform.SetLocalPositionXY(new Vector2(0, HalfFieldSize.Y));
        BoxRenderer2dComponent topWallRenderer = _topWall.AddComponent<BoxRenderer2dComponent>();
        topWallRenderer.SetSize(new Vector2(FieldSize.X + FieldThickness, FieldThickness));
        topWallRenderer.SetColor(FieldColor);
        PhysicsBody2dComponent topWallBody = _topWall.AddComponent<PhysicsBody2dComponent>();
        topWallBody.SetType(PhysicsBody2dType.Kinematic);
        topWallBody.CreateBox(topWallRenderer.Size, true);
        
        _bottomWall.Transform.SetLocalPositionXY(new Vector2(0, -HalfFieldSize.Y));
        BoxRenderer2dComponent bottomWallRenderer = _bottomWall.AddComponent<BoxRenderer2dComponent>();
        bottomWallRenderer.SetSize(new Vector2(FieldSize.X + FieldThickness, FieldThickness));
        bottomWallRenderer.SetColor(FieldColor);
        PhysicsBody2dComponent bottomWallBody = _bottomWall.AddComponent<PhysicsBody2dComponent>();
        bottomWallBody.SetType(PhysicsBody2dType.Kinematic);
        bottomWallBody.CreateBox(bottomWallRenderer.Size, true);
    }

    void CreatePlayers()
    {
        _leftPlayer = Engine.Entities.CreateWorld("Left Player");
        _rightPlayer = Engine.Entities.CreateWorld("Right Player");
        
        _leftPlayer.Transform.SetLocalPositionXY(new Vector2(-HalfFieldSize.X * PlayerHorizontalPositionFieldSizeMultiplier, 0));
        BoxRenderer2dComponent _leftPlayerRenderer = _leftPlayer.AddComponent<BoxRenderer2dComponent>();
        _leftPlayerRenderer.SetSize(PlayerSize);
        _leftPlayerRenderer.SetColor(FieldColor);
        PhysicsBody2dComponent leftPlayerBody = _leftPlayer.AddComponent<PhysicsBody2dComponent>();
        leftPlayerBody.SetType(PhysicsBody2dType.Kinematic);
        leftPlayerBody.CreateBox(_leftPlayerRenderer.Size, true);
        
        _rightPlayer.Transform.SetLocalPositionXY(new Vector2(HalfFieldSize.X * PlayerHorizontalPositionFieldSizeMultiplier, 0));
        BoxRenderer2dComponent _rightPlayerRenderer = _rightPlayer.AddComponent<BoxRenderer2dComponent>();
        _rightPlayerRenderer.SetSize(PlayerSize);
        _rightPlayerRenderer.SetColor(FieldColor);
        PhysicsBody2dComponent rightPlayerBody = _rightPlayer.AddComponent<PhysicsBody2dComponent>();
        rightPlayerBody.SetType(PhysicsBody2dType.Kinematic);
        rightPlayerBody.CreateBox(_rightPlayerRenderer.Size, true);
    }

    void CreateBall()
    {
        _ball = Engine.Entities.CreateWorld("Ball");
        _ball.Transform.SetLocalPositionXY(new Vector2(0, 0));
        CircleRenderer2dComponent _ballRenderer = _ball.AddComponent<CircleRenderer2dComponent>();
        _ballRenderer.SetRadius(BallRadius);
        _ballRenderer.SetColor(FieldColor);
        PhysicsBody2dComponent ballBody = _ball.AddComponent<PhysicsBody2dComponent>();
        ballBody.SetType(PhysicsBody2dType.Dynamic);
        ballBody.SetGravityScale(0f);
        ballBody.CreateCircle(_ballRenderer.Radius, true);
        ballBody.OnCollisionBegin += WhenBallCollisionBegin;
    }

    void UpdateCamera()
    {
        _camera2dComponent!.FitBoundsToScreen(FieldCameraBounds);
    }

    void UpdatePlayersPosition()
    {
        float leftPlayerDirection = 0f;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) leftPlayerDirection += 1;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) leftPlayerDirection -= 1;
        
        float rightPlayerDirection = 0f;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP)) rightPlayerDirection += 1;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) rightPlayerDirection -= 1;

        float frameTime = Engine.Framerate.GetFrameTimeSeconds();
        float leftPlayerMovement = leftPlayerDirection * PlayersSpeed * frameTime;
        float rightPlayerMovement = rightPlayerDirection * PlayersSpeed * frameTime;

        float newLeftPlayerPosition = _leftPlayer!.Transform.LocalPosition.ToVector2XY().Y
                                      + leftPlayerMovement;

        newLeftPlayerPosition = CapPlayerPosition(newLeftPlayerPosition);
        
        float newRightPlayerPosition = _rightPlayer!.Transform.LocalPosition.ToVector2XY().Y
                                      + rightPlayerMovement;

        newRightPlayerPosition = CapPlayerPosition(newRightPlayerPosition);
        
        _leftPlayer!.Transform.SetLocalPositionY(newLeftPlayerPosition);
        _rightPlayer!.Transform.SetLocalPositionY(newRightPlayerPosition);
    }

    void UpdateBallPosition()
    {
        if (!_ballCanMove)
        {
            return;
        }
        
        float frameTime = Engine.Framerate.GetFrameTimeSeconds();
        Vector2 movement = _ballDirection * BallSpeed * frameTime;
        _ball!.Transform.AddLocalPositionXY(movement);
    }

    float CapPlayerPosition(float position)
    {
        float playerMaxHeightPosition = HalfFieldSize.Y * PlayerMaxVerticalPositionFieldSizeMultiplier;
        float playerMinHeightPosition = -HalfFieldSize.Y * PlayerMaxVerticalPositionFieldSizeMultiplier;

        return Math.Clamp(position, playerMinHeightPosition, playerMaxHeightPosition);
    }

    void SetRandomBallDirection()
    {
        int horizontalDirection = _randomGenerator.NewInt(0, 2) == 0 ? -1 : 1;
        float angleDegrees = _randomGenerator.NewFloat(-BallBounceOnPlayerAngleHalfRange, BallBounceOnPlayerAngleHalfRange);
        Vector2 direction = MathExtensions.GetDirectionFromAngle(angleDegrees) * horizontalDirection;
        _ballDirection = direction;
    }

    void WhenBallCollisionBegin(
        PhysicsBody2dComponent physicsBody2dComponent1,
        PhysicsBody2dComponent physicsBody2dComponent2
        )
    {
        bool isLeftPlayer = physicsBody2dComponent2.Owner.IsEntityOnParentHierarchy(_leftPlayer!);

        if (isLeftPlayer)
        {
            WhenBallCollidedWithPlayer(true);
            return;
        }
        
        bool isRightPlayer = physicsBody2dComponent2.Owner.IsEntityOnParentHierarchy(_rightPlayer!);

        if (isRightPlayer)
        {
            WhenBallCollidedWithPlayer(false);
            return;
        }
        
        bool isTopWall = physicsBody2dComponent2.Owner.IsEntityOnParentHierarchy(_topWall!);

        if (isTopWall)
        {
            WhenBallCollidedWithTopOrBottomWall();
            return;
        }
        
        bool isBottomWall = physicsBody2dComponent2.Owner.IsEntityOnParentHierarchy(_bottomWall!);

        if (isBottomWall)
        {
            WhenBallCollidedWithTopOrBottomWall();
            return;
        }
        
        bool isLeftWall = physicsBody2dComponent2.Owner.IsEntityOnParentHierarchy(_leftWall!);

        if (isLeftWall)
        {
            WhenBallCollidedWithLeftOrRightWall(true);
            return;
        }
        
        bool isRightWall = physicsBody2dComponent2.Owner.IsEntityOnParentHierarchy(_rightWall!);

        if (isRightWall)
        {
            WhenBallCollidedWithLeftOrRightWall(false);
            return;
        }
    }

    void WhenBallCollidedWithPlayer(bool isLeftPlayer)
    {
        WorldEntity player = isLeftPlayer ? _leftPlayer! : _rightPlayer!;

        float topPosition = player.Transform.WorldPosition.Y + PlayerHalfSize.Y;
        float bottomPosition = player.Transform.WorldPosition.Y - PlayerHalfSize.Y;
        float ballPosition = _ball!.Transform.WorldPosition.Y;

        float normalizedPosition = MathExtensions.GetNormalizedFactor(ballPosition, bottomPosition, topPosition);
        float angleDegrees = 0f;

        if (isLeftPlayer)
        {
            angleDegrees = -BallBounceOnPlayerAngleHalfRange + (BallBounceOnPlayerAngleRange * normalizedPosition);   
        }
        else
        {
            angleDegrees = 180f + BallBounceOnPlayerAngleHalfRange - (BallBounceOnPlayerAngleRange * normalizedPosition); 
        }
        
        Vector2 direction = MathExtensions.GetDirectionFromAngle(angleDegrees);
        
        _ballDirection = direction;
    }
    
    void WhenBallCollidedWithTopOrBottomWall()
    {
        _ballDirection.Y = -_ballDirection.Y;
    }

    void WhenBallCollidedWithLeftOrRightWall(bool isLeftWall)
    {
        _asyncTaskRunner.Run(FinishRound);
    }

    async Task StartRound(CancellationToken cancellationToken)
    {
        _ballCanMove = false;
        
        SetRandomBallDirection();
        
        _ball!.Transform.SetLocalPositionXY(Vector2.Zero);
        
        await StopwatchTimer.Await(1.ToSeconds(), cancellationToken);

        _ballCanMove = true;
    }

    async Task FinishRound(CancellationToken cancellationToken)
    {
        _ballCanMove = false;
        
        await StopwatchTimer.Await(1.ToSeconds(), cancellationToken);

        await StartRound(cancellationToken);
    }
}