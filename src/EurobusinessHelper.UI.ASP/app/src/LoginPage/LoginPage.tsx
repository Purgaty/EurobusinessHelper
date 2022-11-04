import Button from "@mui/material/Button";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import { selectProviders } from "../Layout/Footer/authSlice";
import { challenge, fetchProviders } from "./actions";
import "./LoginPage.scss";

const LoginPage = (props: LoginPageProps) => {
  const dispatch = useAppDispatch();
  const providers = useAppSelector(selectProviders);

  useEffect(() => {
    if (!providers) {
      dispatch(fetchProviders());
    }
  }, [dispatch, providers]);

  return (
    <div className="LoginPage">
      <div className="container">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="72"
          height="72"
          viewBox="0 0 24 24"
        >
          <path d="M12 2C6.486 2 2 6.486 2 12s4.486 10 10 10 10-4.486 10-10S17.514 2 12 2zm1 14.915V18h-2v-1.08c-2.339-.367-3-2.002-3-2.92h2c.011.143.159 1 2 1 1.38 0 2-.585 2-1 0-.324 0-1-2-1-3.48 0-4-1.88-4-3 0-1.288 1.029-2.584 3-2.915V6.012h2v1.109c1.734.41 2.4 1.853 2.4 2.879h-1l-1 .018C13.386 9.638 13.185 9 12 9c-1.299 0-2 .516-2 1 0 .374 0 1 2 1 3.48 0 4 1.88 4 3 0 1.288-1.029 2.584-3 2.915z"></path>
        </svg>

        <div className="buttons">
          {providers?.map((provider) => (
            <Button
              variant="contained"
              key={provider}
              onClick={() => challenge(provider.toLowerCase())}
            >
              {provider}
            </Button>
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
