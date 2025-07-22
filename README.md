
Major Idea: 2D Sidescroller, on the Beach

Sub Ideas:
- Main weapon is a firework launcher
    - Progression system
        - Killing enemies gives experience
            - After getting enough experience level up
        - After leveling up pick one of 3 powerups
            - Power-ups to firework launcher
                - Launch multiple fireworks
                - Large firework radius
                - Lower firework cooldown
            - Power-ups to player
                - Movement speed
                - Health gain
- Enemies spawn outside of the viewable and walk in towards the player.
    - Different Enemy types
    i   - Crab
        - Angry beach go-ers.
        - etc.

Timeline:

- Player
    - VelocityComponent : Accelerate entity
    - PlayerInputComponent : Uses velocityComponent to make entity move with player input.
    - HealthComponent : Adding/Subtracting health
    - HitboxComponent : Communicates interation between entities.
    - AnimatedSpriteComponent
- Enemy
    - VelocityComponet : Accelerate entity
    - PathfindingComponent : Uses VelocityComponent to make entity move with pathfinding logic.
    - HealthComponent : Adding/Subtracting health
    - HitboxComponent : Communicates interation between entities.
    - AnimatedSpriteComponent

- Look and Feel
    - Animation
        - Player and enemies
    - Sound Effects
        - Sound for weapons, movement, enemies, player.
    - Shader Effects
        - Water shader
        - Firework <- brighter
        - Bounce effect in certain places
        - Screenspace effects

- Music
    - OST Gameplay.
    - Main Menu/Credits (maybe)

22 
23
24
25
26
27
28
29
30
31
