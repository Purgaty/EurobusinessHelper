import Button from '@mui/material/Button'
import AuthService from "../Services/AuthService"

const LoginPage = (props: LoginPageProps) => {
  return (
    <div>
      <Button variant="contained" onClick={() => AuthService.challenge('microsoft')}>Microsoft</Button>
      <Button variant="contained" onClick={() => AuthService.challenge('google')}>Google</Button>
      <Button variant="contained" onClick={() => AuthService.challenge('facebook')}>Facebook</Button>
    </div>
  )
}

interface LoginPageProps {
  loggedIn?: boolean
}

export default LoginPage
