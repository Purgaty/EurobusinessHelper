
const closeMenuTimeout = 500;

export const clearCloseMenuTimeout =
    (menuTimeout: NodeJS.Timeout | null, setMenuTimeout: (timeout: NodeJS.Timeout | null) => void) => {
        if (menuTimeout) {
            clearTimeout(menuTimeout);
            setMenuTimeout(null);
        }
    }

export const startCloseMenuTimeout =
    (menuTimeout: NodeJS.Timeout | null, setMenuTimeout: (timeout: NodeJS.Timeout | null) => void, setMenuOpen: (open: boolean) => void) => {
        if (!menuTimeout) {
            setMenuTimeout(setTimeout(() => setMenuOpen(false), closeMenuTimeout));
        }
    }