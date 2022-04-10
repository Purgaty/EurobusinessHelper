import Button from '@mui/material/Button'

const LoginPage = (props: LoginPageProps) => {
  return (
    <div>
      <Button variant="contained">Hello</Button>
    </div>
  )
}

interface LoginPageProps {
  loggedIn?: boolean
}

export default LoginPage
