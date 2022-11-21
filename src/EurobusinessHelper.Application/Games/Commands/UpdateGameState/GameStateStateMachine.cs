using EurobusinessHelper.Application.Common.Exceptions;
using EurobusinessHelper.Domain.Entities;

namespace EurobusinessHelper.Application.Games.Commands.UpdateGameState;

public class GameStateStateMachine
{
    private readonly Game _game;

    private static readonly Dictionary<GameState, GameState> Transitions =
        new()
        {
            [GameState.New] = GameState.Started,
            [GameState.Started] = GameState.Finished,
            [GameState.Finished] = GameState.New,
        };

    public GameStateStateMachine(Game game)
    {
        _game = game;
    }

    public void SetState(GameState state)
    {
        if (Transitions[_game.State] != state)
            throw new StateMachineException(_game.Id, _game.State, state);
        _game.State = state;
    }
}