const prodConfig = {
  apiUrl: "",
};

const devConfig = {
  apiUrl: "http://192.168.1.106:3000",
};

export default process.env.NODE_ENV === "development" ? devConfig : prodConfig;
