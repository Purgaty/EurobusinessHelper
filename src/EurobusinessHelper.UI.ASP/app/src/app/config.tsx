const prodConfig = {
  apiUrl: ''
}

const devConfig = {
  apiUrl: 'http://localhost:5000'
}

export default process.env.NODE_ENV === 'development' ? devConfig : prodConfig;
