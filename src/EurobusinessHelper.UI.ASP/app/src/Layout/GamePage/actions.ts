import GameService from "../../Services/Game/GameService";

export const getGames = async (): Promise<[]> => {
  // const data = await GameService.getGames();
  // console.log(data);

  return await GameService.getGames();
};
