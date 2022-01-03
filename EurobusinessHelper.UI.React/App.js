import { View, Text } from "react-native";

import MainPage from "./MainPage";

export default function App() {
  return (
    <View
      style={{
        flex: 1,
        justifyContent: "center",
        alignItems: "center",
      }}
    >
      <MainPage />
    </View>
  );
}
