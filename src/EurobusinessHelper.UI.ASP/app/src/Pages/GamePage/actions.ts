import GameService from "../../Services/Game/GameService";
import {NewGame} from "../../Services/Game/types";
import {
    selectGameDetails,
    selectGameList,
    selectGameListSearch,
    setGameDetails,
    setGameList,
    setGameListSearch,
    setOpenGameMode,
    setSelectedGame,
} from "./gameSlice";
import {ErrorCodes, GameState, JoinGameData, NewGameForm} from "./types";

export const refreshGames =
    (gameState: GameState, resetSelection = false) =>
        async (dispatch: Function, getState: Function): Promise<void> => {
            const query = selectGameListSearch(getState());
            await dispatch(fetchGames(gameState, query, resetSelection));
        };

export const fetchGames =
    (state: GameState, query: string, resetSelection = false) =>
        async (dispatch: Function): Promise<void> => {
            dispatch(setGameSearch(query));
            const gameList = await GameService.getGames(state, query);
            dispatch(setGameList({state, list: gameList}));
            if (resetSelection)
                dispatch(resetSelectedGame(state))
        };

const resetSelectedGame = (state: GameState) =>
    (dispatch: Function) => {
        if (dispatch(tryResetSelectedGameFromState(state)))
            return;
        const otherState = state === GameState.New ? GameState.Started : GameState.New;
        if (dispatch(tryResetSelectedGameFromState(otherState)))
            return;
        dispatch(setOpenGameMode(GameState.NotCreated))
    }

const tryResetSelectedGameFromState = (state: GameState) =>
    (dispatch: Function, getState: Function): boolean => {
        const list = selectGameList(state)(getState());
        if (list?.length) {
            dispatch(setSelectedGame(list[0].id));
            dispatch(setOpenGameMode(state));
            return true;
        }
        return false;
    }

export const fetchDetails =
    (gameId: string, forceReload: boolean = false) =>
        async (dispatch: Function, getState: Function): Promise<void> => {
            if (!forceReload && !!selectGameDetails(gameId)(getState())) return;
            const details = await GameService.getGameDetails(gameId);
            dispatch(setGameDetails(details));
        };

export const joinGame = async (
    gameId: string,
    password: JoinGameData
): Promise<void> => {
    await GameService.joinGame(gameId, password);
};

export const deleteGame = async (gameId: string): Promise<void> => {
    return await GameService.deleteGame(gameId);
};

export const changeGameState = async (
    gameId: string,
    state: GameState
): Promise<void> => {
    await GameService.changeGameState(gameId, state);
};

export const setGameSearch =
    (search: string) =>
        (dispatch: Function): void => {
            dispatch(setGameListSearch(search));
        };

export const transferMoney = async (
    gameId: string,
    payerId: string,
    receiverId: string,
    amount: number
): Promise<void> => {
    await GameService.transferMoney(gameId, payerId, receiverId, amount);
};

export const getErrorMessage = (error: string) => {
    switch (error) {
        case ErrorCodes.InternalAppError:
            return "Internal error";
        case ErrorCodes.UnauthorizedUser:
            return "User is unauthorized";
        case ErrorCodes.GameNotFound:
            return "Game not found";
        case ErrorCodes.AccountAlreadyExists:
            return "Account already exists";
        case ErrorCodes.InvalidGamePassword:
            return "Incorrect password";
        case ErrorCodes.PasswordNotProvided:
            return "Password not provided";
        case ErrorCodes.GameAccessDenied:
            return "Access denied";
        case ErrorCodes.InvalidGameStateChange:
            return "Invalid game state change";
        case ErrorCodes.CannotJoinNotNewGame:
            return "The game has already started";
        case ErrorCodes.StartingAccountBalanceNotProvided:
            return "Starting accounts balance not provided";
        case ErrorCodes.AccountNotFound:
            return "Account not found";
        case ErrorCodes.InsufficientFunds:
            return "Insufficient funds";
        case ErrorCodes.AccountNotRegistered:
            return "Account is not registered";
        case ErrorCodes.MinimalBankTransferApprovalsNotProvided:
            return "Minimal bank transfer approvals not provided";
        case ErrorCodes.TransferRequestNotFound:
            return "Transfer request not found";
        default:
            return "";
    }
};

export const createNewGame = async (values: NewGameForm): Promise<string> => {
    const game = new NewGame(values);
    return await GameService.createGame(game);
};
