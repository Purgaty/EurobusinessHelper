import Button from "@mui/material/Button";
import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import { selectProviders } from "../Layout/Footer/authSlice";
import { challenge, fetchProviders } from "./actions";

const LoginPage = (props: LoginPageProps) => {
  const dispatch = useAppDispatch();
  const providers = useAppSelector(selectProviders);

  useEffect(() => {
    if (!providers) {
      dispatch(fetchProviders());
    }
  }, [dispatch, providers]);

  return (
    <div>
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
  );
};

interface LoginPageProps {
  loggedIn?: boolean;
}

export default LoginPage;
