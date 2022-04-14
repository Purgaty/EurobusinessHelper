import React, { useCallback } from 'react'
import { BrowserRouter } from 'react-router-dom'
import AppHeader from './Header/AppHeader'
import './AppLayout.scss'
import AppSidebar from './AppSidebar'
import {
  clearCloseMenuTimeout,
  startCloseMenuTimeout
} from './closeMenuTimeout'
import { AppFooter } from './Footer/AppFooter'
import { AppRoutes } from './AppRoutes'

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
    if (!menuOpen) setMenuOpen(true)
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
            <AppRoutes />
          </div>
        </BrowserRouter>
      </div>
      <AppFooter />
    </div>
  )
}

export default AppLayout
