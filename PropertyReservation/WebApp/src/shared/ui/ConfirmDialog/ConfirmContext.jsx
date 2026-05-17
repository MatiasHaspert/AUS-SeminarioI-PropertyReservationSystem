import { createContext, useContext, useState, useCallback } from "react";
import ConfirmDialog from "./ConfirmDialog";

const ConfirmContext = createContext(null);

export function ConfirmProvider({ children }) {
    const [dialog, setDialog] = useState(null);

    const confirm = useCallback((message, options = {}) => {
        return new Promise((resolve) => {
            setDialog({ message, options, resolve });
        });
    }, []);

    const handleResponse = (answer) => {
        dialog?.resolve(answer);
        setDialog(null);
    };

    return (
        <ConfirmContext.Provider value={confirm}>
            {children}
            {dialog && (
                <ConfirmDialog
                    message={dialog.message}
                    {...dialog.options}
                    onConfirm={() => handleResponse(true)}
                    onCancel={() => handleResponse(false)}
                />
            )}
        </ConfirmContext.Provider>
    );
}

export function useConfirm() {
    return useContext(ConfirmContext);
}
