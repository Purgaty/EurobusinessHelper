import axios from "axios";

export const setUpAxios = () => {
  axios.defaults.withCredentials = true;
  configureInterceptors();
};

const configureInterceptors = () => {
  axios.interceptors.response.use(undefined, responseRejected);
};

const responseRejected = (error: any) => {
  const response = error.response;
  if (response.status === 401) {
    if (!window.location.href.endsWith("/login"))
      window.location.href = "/login";

    return Promise.resolve();
  }
  return Promise.reject(error);
};
