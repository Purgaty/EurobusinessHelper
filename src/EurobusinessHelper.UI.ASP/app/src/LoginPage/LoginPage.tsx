import React, { useEffect } from "react";
import { BsFacebook, BsGoogle, BsMicrosoft } from "react-icons/bs";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import { selectProviders } from "../Layout/Footer/authSlice";
import { challenge, fetchProviders } from "./actions";
import { providerNames } from "./consts";
import "./LoginPage.scss";

const LoginPage = (props: LoginPageProps) => {
  const dispatch = useAppDispatch();
  const providers = useAppSelector(selectProviders);

  useEffect(() => {
    if (!providers) {
      dispatch(fetchProviders());
    }
  }, [dispatch, providers]);

  const placeIcon = (provider: string) => {
    switch (provider) {
      case providerNames.facebook:
        return <BsFacebook />;

      case providerNames.google:
        return <BsGoogle />;

      case providerNames.microsoft:
        return <BsMicrosoft />;

      default:
        break;
    }
  };

  return (
    <div className="login-page">
      <div className="container login-container">
        <p>Log In</p>
        <div className="buttons">
          {providers?.map((provider) => (
            <div
              className="provider"
              key={provider}
              onClick={() => challenge(provider.toLowerCase())}
            >
              <div className="provider-icon">
                {placeIcon(provider.toLowerCase())}
              </div>
              <span className="provider-name">{provider}</span>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

interface LoginPageProps {
  loggedIn?: boolean;
}

export default LoginPage;
