import { FaDice } from 'react-icons/fa'
import { AiOutlineClose, AiOutlineMenuUnfold } from 'react-icons/ai'
import { useCallback, useMemo } from 'react'

const AppHeader = ({
  menuOpen,
  setMenuOpen,
  onMenuEnter,
  onMenuLeave
}: AppHeaderProps) => {
  const openMenu = useCallback(() => setMenuOpen(true), [setMenuOpen])
  const closeMenu = useCallback(() => setMenuOpen(false), [setMenuOpen])

  return (
    <div className='application-header'>
      <div
        className={`header-left ${menuOpen ? 'visible' : 'hidden'}`}
        onMouseEnter={onMenuEnter}
        onMouseLeave={onMenuLeave}
      >
        <div className='menu-buttons'>
          <AiOutlineClose
            className={menuOpen ? 'menu-icon' : 'menu-icon hidden'}
            onClick={closeMenu}
          />
          <AiOutlineMenuUnfold
            className={menuOpen ? 'menu-icon hidden' : 'menu-icon'}
            onClick={openMenu}
          />
        </div>
        <div className='menu-title' onClick={closeMenu}>
          MENU
        </div>
      </div>
      <div className='header-right'>
        <FaDice className='header-icon' />
        <p className='title'>Eurobusiness helper</p>
      </div>
    </div>
  )
}

interface AppHeaderProps {
  menuOpen: boolean
  setMenuOpen: (open: boolean) => void
  onMenuEnter: () => void
  onMenuLeave: () => void
}

export default AppHeader
