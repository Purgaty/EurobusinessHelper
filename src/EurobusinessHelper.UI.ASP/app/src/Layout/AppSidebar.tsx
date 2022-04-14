import { Link } from 'react-router-dom'


const AppSidebar = ({ menuOpen, onMenuEnter, onMenuLeave }: AppSidebarProps) => {
  return (
    <div
      className={`application-sidebar ${menuOpen ? 'visible' : 'hidden'}`}
      onMouseLeave={onMenuLeave}
      onMouseEnter={onMenuEnter}
    >
      <p>TO JEST SIDEBAR</p>
      <Link to='/'>Home</Link>
      <Link to='/users'>Users</Link>
      <Link to='/games'>Games</Link>
      <Link to='/login'>Login</Link>
    </div>
  )
}

interface AppSidebarProps {
  menuOpen: boolean,
  onMenuEnter: () => void,
  onMenuLeave: () => void
}

export default AppSidebar
