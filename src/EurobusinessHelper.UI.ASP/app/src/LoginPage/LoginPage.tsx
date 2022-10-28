import Button from '@mui/material/Button';
import { useEffect, useState } from 'react';
import { challenge, getProviders } from './actions';

const LoginPage = (props: LoginPageProps) => {
  const [providers, setProviders] = useState<string[]>([]);
 
  useEffect(() => {
    getProviders().then(p => {
      setProviders(p);
    });
  },[]);

  return (
    <div>
      {providers.map(provider => (<Button variant="contained" key={provider} onClick={() => challenge(provider.toLowerCase())}>{provider}</Button>))}
    </div>
  )
}

interface LoginPageProps {
  loggedIn?: boolean
}

export default LoginPage
