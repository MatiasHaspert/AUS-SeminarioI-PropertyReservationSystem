import { AuthProvider } from "@/shared/auth/AuthContext";
import { ToastProvider } from "@/shared/ui/Toast";
import { ConfirmProvider } from "@/shared/ui/ConfirmDialog";

export default function AppProviders({ children }) {
    return (
        <ToastProvider>
            <ConfirmProvider>
                <AuthProvider>{children}</AuthProvider>
            </ConfirmProvider>
        </ToastProvider>
    );
}
