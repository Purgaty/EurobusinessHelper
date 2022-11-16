import GameService from "../../Services/Game/GameService";

export const getGames = async (query: string): Promise<[]> => {
  return await GameService.getGames(query);
};
