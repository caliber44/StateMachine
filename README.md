# State Machine for Unity

This is a simple state machine implementation for Unity, allowing for modular and scalable state management in your game.

## Features
- Supports multiple states using an interface (`IState`)
- Fixed update rate to ensure consistent logic execution
- Ability to switch and revert states
- Handles frame rate drops by catching up on missed updates
- States provide independant tick rates
- Control when transitions are possible or force change
- Pause/Play
- Adjustable Global Tick Time Scale
