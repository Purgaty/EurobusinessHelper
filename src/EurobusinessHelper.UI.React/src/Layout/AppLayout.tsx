import React, { useCallback } from 'react'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import LoginPage from '../LoginPage/LoginPage'
import AppHeader from './AppHeader'
import './AppLayout.scss'
import AppSidebar from './AppSidebar'
import {
  clearCloseMenuTimeout,
  startCloseMenuTimeout
} from './closeMenuTimeout'

const AppLayout = () => {
  const [menuOpen, setMenuOpen] = React.useState(false)
  const [
    closeMenuTimeout,
    setCloseMenuTimeout
  ] = React.useState<NodeJS.Timeout | null>(null)
  const onMenuLeave = useCallback(
    () =>
      startCloseMenuTimeout(closeMenuTimeout, setCloseMenuTimeout, setMenuOpen),
    [setMenuOpen, closeMenuTimeout, setCloseMenuTimeout]
  )
  const onMenuEnter = useCallback(() => {
    clearCloseMenuTimeout(closeMenuTimeout, setCloseMenuTimeout)
    if (!menuOpen)
      setMenuOpen(true)
  }, [closeMenuTimeout, setCloseMenuTimeout, menuOpen])

  return (
    <div className='application-container'>
      <AppHeader
        menuOpen={menuOpen}
        setMenuOpen={setMenuOpen}
        onMenuEnter={onMenuEnter}
        onMenuLeave={onMenuLeave}
      />
      <div className='application-body'>
        <BrowserRouter>
          <AppSidebar
            menuOpen={menuOpen}
            onMenuEnter={onMenuEnter}
            onMenuLeave={onMenuLeave}
          />
          <div className='application-content'>
            <Routes>
              <Route path='/' element={<p>HOME</p>} />
              <Route
                path='/users'
                element={
                  <div>
                    <p>USERS</p>
                  </div>
                }
              />
              <Route path='/games' element={<p>GAMES</p>} />
              <Route path='/login' element={<LoginPage />} />
            </Routes>
          </div>
        </BrowserRouter>
      </div>
      <div className='application-footer'>FOOTER</div>
    </div>
  )
}

export default AppLayout
